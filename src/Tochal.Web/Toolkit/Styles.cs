namespace Tochal.Toolkit
{
    public static class Styles
    {
        public static string MakeLayoutBlank()
        {
            return @"
                <style>
                    .normalheader,
                    #menu,
                    footer.footer,
                    [data-role='in-progress-warning'],
                    [data-role='hidden-in-view'],
                    #header {
                        display: none;
                    }

                    #wrapper {
                        margin-right: 0;
                        top: 0 !important;
                    }

                    body {
                        background: #f1f3f6;
                    }
                    
                    .hpanel--fullheight .panel-body {
                        min-height: calc(100vh - 50px);
                    }
                    .content { 
                        padding-bottom: 0; 
                    }
                    #wrapper {
                        transition: none !important;
                    }
                </style>
            ";
        }
    }
}