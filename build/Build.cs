using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    [Solution] readonly Solution Solution;
    AbsolutePath PublishDirectory => RootDirectory / "publish";
    AbsolutePath SrcDirectory => RootDirectory / "src";
    AbsolutePath TestDirectory => RootDirectory / "test";
    
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            var deletableDirectories = SrcDirectory.GlobDirectories("**/obj", "**/bin").ToList();
            deletableDirectories.ForEach(DeleteDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s.SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
            DotNetBuild(settings =>
                settings.SetProjectFile(Solution).EnableNoRestore()));
    
    Target Test => _ => _
        // .DependsOn(Compile)
        .Executes(() =>
            DotNetTest(settings => settings.SetProjectFile(TestDirectory)
                .EnableNoRestore()
                .EnableNoBuild()));
    Target Publish => _ => _
         .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetPublish(settings =>
                settings.SetProject(Solution)
                    .SetOutput(PublishDirectory)
                    .EnableNoRestore()
                    .EnableNoBuild()
            );
        });
}
