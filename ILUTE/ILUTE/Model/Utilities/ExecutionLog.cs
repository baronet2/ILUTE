﻿/*
    Copyright 2016 Travel Modelling Group, Department of Civil Engineering, University of Toronto

    This file is part of ILUTE, a set of modules for XTMF.

    XTMF is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    XTMF is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with XTMF.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMG.Input;
using XTMF;

namespace TMG.Ilute.Model.Utilities
{
    [ModuleInformation(Description = "This module provides a simple way to log the results from the model system to file.")]
    public sealed class ExecutionLog : IDataSource<ExecutionLog>, IDisposable
    {
        [SubModelInformation(Required = true, Description = "The location to save the log to.")]
        public FileLocation SaveTo;

        [RunParameter("Append to log", true, "Append the new log if the file already exists.")]
        public bool Append;

        private StreamWriter Writer;

        public bool Loaded { get; set; }

        public string Name { get; set; }

        public float Progress { get; set; }

        public Tuple<byte, byte, byte> ProgressColour { get { return new Tuple<byte, byte, byte>(50, 150, 50); } }

        public void Dispose()
        {
            UnloadData();
        }

        public ExecutionLog GiveData()
        {
            return this;
        }

        public void LoadData()
        {
            lock (this)
            {
                // make sure that the data would have already been saved
                if(Writer != null)
                {
                    UnloadData();
                }
                Writer = new StreamWriter(SaveTo, Append);
                Loaded = true;
            }
        }

        /// <summary>
        /// Save a message to the log.
        /// </summary>
        /// <param name="toLog">The message to write to the log.</param>
        public void WriteToLog(string toLog)
        {
            if (Writer != null)
            {
                var currentTime = DateTime.Now;
                Writer.Write('[');
                Writer.Write(currentTime.Hour);
                Writer.Write(':');
                Writer.Write(currentTime.Minute);
                Writer.Write(':');
                Writer.Write(currentTime.Second);
                Writer.Write("] ");
                Writer.WriteLine(toLog);
            }
        }

        public bool RuntimeValidation(ref string error)
        {
            return true;
        }

        public void UnloadData()
        {
            lock (this)
            {
                if (Writer != null)
                {
                    Writer.Dispose();
                    Writer = null;
                }
                Loaded = false;
            }
        }
    }
}
