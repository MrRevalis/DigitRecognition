using Ninject;

namespace DigitRecognition.Core
{
    public static class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static void Setup()
        {
            BindViewModel();
        }

        private static void BindViewModel()
        {
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
            Kernel.Bind<LearningViewModel>().ToConstant(new LearningViewModel());
            Kernel.Bind<RecognitionViewModel>().ToConstant(new RecognitionViewModel());
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
