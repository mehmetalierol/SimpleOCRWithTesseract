using Ninject;
using Ninject.Parameters;
using System;

namespace SimpleOCR.Core
{
    public class Kernel
    {
        //Singleton threadsafe
        private static readonly Lazy<Kernel> LazyKernel = new Lazy<Kernel>(() => new Kernel());

        public static Kernel Instance => LazyKernel.Value;

        public Kernel()
        {
            Initialize();
        }

        private IKernel _kernel;
        public void Initialize()
        {
            _kernel = new StandardKernel(new SimpleOCRModule());
        }

        public T GetType<T>()
        {
            return _kernel.Get<T>();
        }

        public T GetType<T>(params IParameter[] parameters)
        {
            return _kernel.Get<T>(parameters);
        }
    }
}
