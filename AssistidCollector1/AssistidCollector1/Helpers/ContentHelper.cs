//----------------------------------------------------------------------------------------------
// <copyright file="ContentHelper.cs" 
// Copyright February 2, 2018 Shawn Gilroy
//
// This file is part of AssistidCollector2
//
// AssistidCollector2 is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3.
//
// AssistidCollector2 is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with AssistidCollector2.  If not, see http://www.gnu.org/licenses/. 
// </copyright>
//
// <summary>
// The AssistidCollector2 is a tool to assist clinicans and researchers in the treatment of communication disorders.
// 
// Email: shawn(dot)gilroy(at)temple.edu
//
// </summary>
//----------------------------------------------------------------------------------------------

using AssistidCollector1.Enums;
using AssistidCollector1.Models;
using System.Collections.Generic;

namespace AssistidCollector1.Helpers
{
    public class ContentHelper
    {
        /// <summary>
        /// Add Sleep Relaxation Strategies
        /// </summary>
        /// <param name="currentSleepTasks"></param>
        /// <returns></returns>
        public static List<SleepTasks> AddSleepRelaxationContent(List<SleepTasks> currentSleepTasks)
        {
            currentSleepTasks.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Relaxation,
                PageTitle = "Relaxation Technique 1",
                PageDescription = "Help your child relax before bed by reading a book, give a gentle back massage, or turn on soft music.",
            });

            currentSleepTasks.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Relaxation,
                PageTitle = "Relaxation Technique 2",
                PageDescription = "Show your child how to take a deep breath and hold it for 5 seconds, and then exhale slowly. Practice this with your child and when you think your child has got it right add the next step. This time ask your child to take a deep breath as practiced and when your child is holding their breath for the 5 seconds you say to your child \"relax and let your breath out slowly\".  Get your child to repeat this for 5 breaths. Supervise closely until you are sure your child can do it correctly.",
            });

            currentSleepTasks.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Relaxation,
                PageTitle = "Relaxation Technique 3",
                PageDescription = "Help your child to select a picture of a quiet, calm place and talk to your child about this place. Find out from your child why they think it is  a calm place. Make sure it is somewhere that your child has selected as calming for them. Use the words your child has provided to describe this place. Speak slowly and use a soft voice when you  describe the place and remember use your child’s words.  This will create a relaxing feeling. After you have practiced this with your child you can then write a short script to help you consistently describe this place. At this stage you can ask your child to close their eyes and you can slowly and softly describe the place for the child. It is possible with practice for your child to progress to doing this on their own.",
            });

            return currentSleepTasks;
        }

        /// <summary>
        /// Add Sleep Hygiene Strategies
        /// </summary>
        /// <param name="currentSleepTasks"></param>
        /// <returns></returns>
        public static List<SleepTasks> AddSleepHygieneContent(List<SleepTasks> currentSleepTasks)
        {
            currentSleepTasks.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Sleep Environment",
                PageDescription = "The bedroom environment should be dark, quiet, and cool to minimize distractions. Children with autism may be particularly sensitive to these sources of distraction.",
            });

            currentSleepTasks.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Bedtime Routine",
                PageDescription = "Develop a consistent timeline for bedtime routines and stick to them.",
            });

            currentSleepTasks.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Establish a Reasonable Bedtime",
                PageDescription = "Tell your child when it is and, under normal circumstances, put your child to bed at that time every night.",
            });

            currentSleepTasks.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Quiet Time",
                PageDescription = "Institute a \"Quiet Time\" 20-30 minutes before bedtime. During this time, your child can engage in quiet activities such as reading while avoiding activities such as screen time.",
            });

            currentSleepTasks.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Preparing for Bed",
                PageDescription = "A bedtime routine involves preparing for bed (such as getting into pajamas, brushing teeth, going to the bathroom, last drink of water) and activities to relax your children. The bedtime routine, once established, will become a powerful signal to your child that the time for sleep has come.",
            });

            currentSleepTasks.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Sleep and Wake schedule",
                PageDescription = "The schedule should be regular with not much of a difference between the weekday and weekend schedule.",
            });

            currentSleepTasks.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Morning Awakening Time",
                PageDescription = "Keep morning awakening time consistent and reduce or avoid daytime naps so the child does not 'make up' for lost sleep following a difficult night. This will increase the likelihood that they will fall asleep more quickly the next evening.",
            });

            currentSleepTasks.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Avoid caffeine",
                PageDescription = "Avoid caffeine particularly close to bedtime, as it could make it more difficult for your child to fall asleep. Caffeine is often found in tea, chocolate and fizzy drinks.",
            });

            return currentSleepTasks;
        }
    }
}
