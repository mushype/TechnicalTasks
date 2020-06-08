This console application will read in a CSV file and convert to both Json and XML.

To run the application you need to specify three parameters...
- The Source file of the CSV
- The Destination file that is written upon conversion
- The Format of the conversion - This is either 'xml' or 'json'
For example 
>CSVReaderTask.exe C:\Users\peter.gooch\source\GitHub\TechnicalTasks\CSVReaderTask\CSVReaderTask\data c:\temp\xmlDataoutput.xml xml

The CSV file I have used in this example can be found in the \Data directory

If you run this in debug mode then you can add the application arguments from the debug properties of the project.

