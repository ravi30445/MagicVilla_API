namespace MagicVilla_webapi.Logging
{
    public class LoggingV2:ILogging{
            public void Log(string message,string type){
                if(type=="error"){
                    Console.WriteLine("ERROR --"+message);
                }
                else{
                    if(type=="warning"){
                       Console.WriteLine("error"+message); 
                    }
                else{
                    Console.WriteLine(message);
                    }
                }
             }
    }

    
}