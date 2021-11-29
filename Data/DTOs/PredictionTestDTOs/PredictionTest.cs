using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Envisio.Data.DTOs.PredictionTestDTOs
{
    public class PredictionTest
    {
        public double RadiusMean { get; set; }
        public double TextureMean { get; set; }
        public double PerimeterMean { get; set; }
        public double AreaMean { get; set; }
        public double CompactnessMean { get; set; }
        public double ConcavityMean { get; set; }
        public double PerimeterSe { get; set; }
        public double AreaSe  { get; set; }
        public double RadiusWorst { get; set; }
        public double TextureWorst  { get; set; }
        public double PerimeterWorst { get; set; }
        public double AreaWorst { get; set; }
        public double CompactnessWorst  { get; set; }
        public double ConcavityWorst   { get; set; }
    }
}
