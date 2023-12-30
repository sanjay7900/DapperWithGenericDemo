using System;
using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace DapperWithGenericDemo
{
    public  class Img
    {

       public Img()
        {
            // Load two images
            Bitmap image1 = new Bitmap("C:/Users/Lenovo/oneDrive/Pictures/Camera Roll/WIN_20221215_09_45_09_Pro.jpg");
            Bitmap image2 = new Bitmap("C:/Users/Lenovo/OneDrive/Pictures/Camera Roll/WIN_20221215_09_47_31_Pro.jpg");

            // Convert images to grayscale
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayImage1 = filter.Apply(image1);
            Bitmap grayImage2 = filter.Apply(image2);

            // Compute Structural Similarity Index (SSI)
            ExhaustiveTemplateMatching templateMatching = new ExhaustiveTemplateMatching(0);
            TemplateMatch[] matches = templateMatching.ProcessImage(grayImage1, grayImage2);

            // Calculate percentage match
            double matchPercentage = matches[0].Similarity * 100;

            Console.WriteLine($"Percentage Match: {matchPercentage}%");
        }

    }
}
