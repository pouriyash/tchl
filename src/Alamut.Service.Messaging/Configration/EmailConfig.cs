namespace Service.Messaging.Configration
{
    public class EmailConfig
    {
        public string From { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }

        public bool SSL { get; set; }

        /// <summary>
        /// نام ایمیل ارسال کننده
        /// مثلا سامانه جامع پژوهشی مپفا
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ارسال به ایمیل خاص 
        ///جهت بازخورد
        /// </summary>
        public string To { get; set; }
    }
}
