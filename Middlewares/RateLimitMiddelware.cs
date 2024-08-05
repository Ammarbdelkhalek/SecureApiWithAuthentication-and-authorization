namespace SecureApiWithAuthentication.Middlewares
{
    public class RateLimitMiddelware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RateLimitMiddelware> logging;
        private static int counter = 0;
        private static DateTime time = DateTime.Now;
        

        public RateLimitMiddelware(RequestDelegate next , ILogger<RateLimitMiddelware> logging)  
        {
            this.next = next;
            this.logging = logging;
        }

        public async Task Invoke (HttpContext context)
        {
            counter++;

            if(DateTime.Now.Subtract(time).Seconds > 10)
            {
                counter = 1;    
                time = DateTime.Now;
                await next(context); 
            }
            else
            {
                if(counter > 5)
                {
                    time = DateTime.Now;
                    await context.Response.WriteAsync("request limit exceeed");

                }
                else
                {
                    time = DateTime.Now;
                    await next(context);
                }
            }

        }


    }
}
