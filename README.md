# Time Logger App

## Designing using the **WOULD** Framework

### **W** - Walk through it

A small application that will be used to track
hours spent working for a client, the billables
associated with those hours, and the payments
made for those hours.

### **O** - Open Up the Requirements

- WPF Front-End (no MWM)
- SQLite Backend
- Small as possible
- Simple

### **U** - User Interface Design

### **L** - Logic Design

- Minimize to System Tray
- Deploy SQLite with the application
- Deploy application using copy/paste
- WPF Parent/Child forms
- WPF Menu Bar

### **D** - Database Design

**Bold** columns have default values.

 | Client                   | Defaults             | Payments | Work        |
 | ------------------------ | -------------------- | -------- | ----------- |
 | Name                     | HourlyRate           | ClientId | ClientId    |
 | Email                    | PreBill              | Hours    | Hours       |
 | **HourRate**             | HasCutOff            | Amount   | Title       |
 | **PreBill**              | CutOff               | Date     | Description |
 | **HasCutOff**            | MinimumHours         |          | Date        |
 | **CutOff**               | Billinglncrement     |          | Paid        |
 | **MinimumHours**         | RoundUpAfterXMinutes |          | PaymentId   |
 | **BillingIncrement**     |                      |          |             |
 | **RoundUpAfterXMinutes** |                      |          |             |







### DefaultControl Notes

1. Validate form when submit button clicked
2. Save model to db if valid
3. Notify user if invalid
