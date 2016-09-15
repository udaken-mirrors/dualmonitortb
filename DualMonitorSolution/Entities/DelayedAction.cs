using System;
using System.Threading;
using System.Windows.Forms;

namespace DualMonitor.Entities
{
    public class DelayedAction
    {
        public bool Active { get; private set; }
        private readonly Form _form;

        public DelayedAction(Form form)
        {
            _form = form;
        }

        public void Init(Action activate, int delay)
        {
            Active = true;

            ThreadPool.QueueUserWorkItem(delegate
            {
                DateTime now = DateTime.Now;

                if (delay > 100)
                {
                    while (this.Active)
                    {
                        Thread.Sleep(100);

                        if ((DateTime.Now - now).TotalMilliseconds >= delay)
                        {
                            break;
                        }
                    }
                }

                if (this.Active)
                {
                    try
                    {
                        _form.Invoke(new MethodInvoker(activate));
                    }
                    catch
                    {
                        // ignored
                    }
                    this.Active = false;
                }
            });
        }

        public void Cancel()
        {
            Active = false;
        }
    }
}
