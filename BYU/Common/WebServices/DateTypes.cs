namespace Common.WebServices
{
    //contains the date types used by the 'get control dates' web service.
    //note that the string values passed in the URL are case-insensitive, meaning enum.toString can safely be used
    //note that I have been unable to find a comprehensive list of all date types
    internal enum DateType
    {
        Semester,   //1st day of cls to last day of finals
        Current_YYT,//1st day of cls to last day of finals, meaning of yyt is unclear; 
        Finals,     //1st day of finals to last day of finals
        Curriculum  //1st day of class to day before next 1st day of class
    }
}
