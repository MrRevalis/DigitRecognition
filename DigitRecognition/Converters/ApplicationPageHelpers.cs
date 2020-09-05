using DigitRecognition.Core;
using DigitRecognition.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitRecognition.Converters
{
    public static class ApplicationPageHelpers
    {
        public static BasePage ToBasePage(this ApplicationPage applicationPage, object viewModel = null)
        {
            switch (applicationPage)
            {
                case ApplicationPage.RecognitionPage:
                    return new RecognitionPage(viewModel as RecognitionViewModel);
                case ApplicationPage.LearningPage:
                    return new LearningPage(viewModel as LearningViewModel);
                default:
                    return null;
            }
        }

        public static ApplicationPage ToApplicationPage(this BasePage page)
        {
            if (page is LearningPage)
                return ApplicationPage.LearningPage;

            if (page is RecognitionPage)
                return ApplicationPage.RecognitionPage;

            return default(ApplicationPage);
        }
    }
}
