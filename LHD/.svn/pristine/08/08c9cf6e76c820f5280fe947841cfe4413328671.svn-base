using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Threading;
using Quartz;
using Quartz.Impl;
using System.IO;

namespace XJP.BPM.AMSService
{
    class MainEntry
    {
        static IScheduler sched = null;
        public void Run()
        {
            try
            {
                Log.Instance.Info("------- Initializing ----------------------");
                // First we must get a reference to a scheduler
                NameValueCollection properties = new NameValueCollection();
                properties["quartz.scheduler.instanceName"] = "XmlConfiguredInstance";
                // set thread pool info
                properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
                properties["quartz.threadPool.threadCount"] = "50";
                properties["quartz.threadPool.threadPriority"] = "Normal";
                // job initialization plugin handles our xml reading, without it defaults are used
                properties["quartz.plugin.xml.type"] = "Quartz.Plugin.Xml.JobInitializationPlugin, Quartz";
                properties["quartz.plugin.xml.fileNames"] = "~quartz_jobs.xml";
                ISchedulerFactory sf = new StdSchedulerFactory(properties);
                sched = sf.GetScheduler();
                Log.Instance.Info("------- Initialization Complete -----------");
                // all jobs and triggers are now in scheduler
                // Start up the scheduler (nothing can actually run until the 
                // scheduler has been started)
                sched.Start();
                try
                {
                    Thread.Sleep(3 * 1000);
                }
                catch (ThreadInterruptedException)
                {
                }
                Log.Instance.Info("------- Shutting Down ---------------------");
                Log.Instance.Info("------- Shutdown Complete -----------------");
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex);
            }
        }
    }
}
