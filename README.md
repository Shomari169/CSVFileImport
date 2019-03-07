# Import-CSV-File-App
An ASP.NET Web Forms CSV Importer system built by Shomari Mensah
Requirements
The application must: 
• Import a CSV file from either: A specified folder/ A web page upload
• Or Both options [Optional]
• Include at least one sample CSV file that is > 1000 lines in length(Source of creating mock data:https://mockaroo.com/)
• 5 or more columns per line
The Design
Column Types to include/Concept
• String – Name of customer
• Date – date of payments 
• Double - amount
• Integer – account number
• GUID – Person Id/Account Id
• with appropriate validation - error handling / confirms when files is loaded.
• Imported files should be stored persistently in an RDBMS
• Display the persisted contents within the browser – Data Grid/View

