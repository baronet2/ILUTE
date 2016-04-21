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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMG.Ilute.Data.Repositories;
using XTMF;


namespace TMG.Ilute.Model.Demographic
{

    public class AgePopulation : IExecuteYearly
    {

        public string Name { get; set; }

        public float Progress { get; set; }

        public Tuple<byte, byte, byte> ProgressColour { get { return new Tuple<byte, byte, byte>(50, 150, 50); } }

        public IResource PersonRepository;

        public void AfterYearlyExecute(int year)
        {

        }

        public void BeforeFirstYear(int firstYear)
        {
        }

        public void BeforeYearlyExecute(int year)
        {
        }

        public void Execute(int year)
        {
            var repo = PersonRepository.AcquireResource<PersonRepository>();
            foreach(var person in repo)
            {
                person.Age += 1;
            }
        }

        public void RunFinished(int finalYear)
        {
        }

        public bool RuntimeValidation(ref string error)
        {
            if (!PersonRepository.CheckResourceType<PersonRepository>())
            {
                error = "In '" + Name + "' the person repository was not of type PersonRepository!";
                return false;
            }
            return true;
        }
    }
}
