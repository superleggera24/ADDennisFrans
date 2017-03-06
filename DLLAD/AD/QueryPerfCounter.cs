﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//make calls to native Win32 functions
using System.Runtime.InteropServices;

namespace AD
{
    // Query Performance Counter class, meet de duur van de uit te voeren sorteermethode
    public class QueryPerfCounter
    {
        //call de QueryPerformanceCounter win32 API
        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        // call de QueryPerformanceFrequenc win32 API
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);



        private long frequency;

        public QueryPerfCounter()
        {
            if (QueryPerformanceFrequency(out frequency) == false)
            {
                // Frequency not supported
                throw new Win32Exception();
            }
        }

        //Start methode om current value van QueryPerformanceCounter te krijgen
        public void Start()
        {
            QueryPerformanceCounter(out start);
        }

        //Stop methode om current value van QueryPerformanceCounter te krijgen
        public void Stop()
        {
            QueryPerformanceCounter(out stop);
        }

        //Duration method met aantal iteraties als argument
        // returned duration value
        // De methode berekent het aantal ticks tussen de start en stop values
        // stop-start resultaat x multiplier (duration van alle operations)
        // deel door het aantal iteraties om de duration per operation value te bekijken

        public double Duration(int iterations)
        {
            return ((((double)(stop - start) *
                      (double)multiplier) /
                      (double)frequency) / iterations);
        }

    }
}