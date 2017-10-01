using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using RayTracer;
using RayTracer.Geometry;
using RayTracer.Lights;
using Color = RayTracer.Color;
using Object = RayTracer.Object;

namespace Executable {
    class Program {
        static void Main() {
            var objects = new List<RayTracer.Object>();
            
            Material sphereMaterial = new Material(
                    new Color(0.1, 0.1, 0.1),
                    new Color(0.0, 0.0, 1.0),
                    new Color(0.0, 0.0, 1.0),
                    new Color(0.0, 0.0, 1.0),
                    new Color(1.0, 1.0, 1.0),
                    0.4, 0.2, 0.2, 5.0, 0.2
                );
            
            Material sphere2Material = new Material(
                new Color(0.1, 0.1, 0.1),
                new Color(1.0, 0.0, 0.0),
                new Color(1.0, 0.0, 0.0),
                new Color(1.0, 0.0, 0.0),
                new Color(1.0, 0.0, 0.0),
                0.4, 0.2, 0.2, 5.0, 0.2
            );
            
            Material glassMaterial = new Material(
                new Color(0.1, 0.1, 0.1),
                new Color(0.9, 0.9, 0.9),
                new Color(0.9, 0.9, 0.9),
                new Color(0.9, 0.9, 0.9),
                new Color(1.0, 1.0, 1.0),
                0.01, 0.04, 0.05, 5.0, 0.1, 1.0, 2.0
            );
            
            Material planeMaterial = new Material(
                new Color(0.1, 0.1, 0.1) * 0.8,
                new Color(0.0, 1.0, 0.0),
                new Color(0.0, 1.0, 0.0),
                new Color(0.0, 1.0, 0.0),
                new Color(0.0, 1.0, 0.0),
                0.8, 0.0, 0.0, 5.0, 0.2
            );
            
            var sphere = new Sphere(new Vector(0, 0, -1), 1.0);
            var sphere2 = new Sphere(new Vector(0, 0, 1), 1.0);
            var glassSphere = new Sphere(new Vector(-1.5, 0, 0), 0.7);
            var plane = new Plane(new Vector(0, 1, 0), new Vector(0, -1, 0));

            var lights = new List<Light>();
            lights.Add(new DirectedLight(new Vector(1, -1, -1).Normalized(), new Color(1, 1, 1)));
            
            objects.Add(new Object(sphere, sphereMaterial));
            objects.Add(new Object(sphere2, sphere2Material));
            objects.Add(new Object(glassSphere, glassMaterial));
            objects.Add(new Object(plane, planeMaterial));
            
            var scene = new Scene(objects, lights, new Color(0.2, 0.2, 0.2), background: new Color(0.2, 0.3, 0.7));
            
            var tracer = new Tracer(scene);
            
            var viewplane = new ViewPlane();

            viewplane.Height = 1000; viewplane.Width = 1000;

            viewplane.Ratio = (double)viewplane.Width / (double)viewplane.Height;

            viewplane.Distance = 2;
            
            Vector cameraPos = new Vector(-10, 0, 0), cameraTarget = new Vector(0, 0, 0);
            Vector cameraUp = new Vector(0, 1, 0);
            
            var camera = new Camera(
                tracer, 
                viewplane,
                cameraPos, cameraTarget, cameraUp,
                maxDepth: 5
            );

            var result = camera.Render();

            var bitmap = new Bitmap(camera.ViewPlane.Width, camera.ViewPlane.Height);

            for (int i = 0; i < viewplane.Width; i++) {
                for (int j = 0; j < viewplane.Height; j++) {
                    var c = result[i, j].Clip();
                    var col = System.Drawing.Color.FromArgb(
                        (byte) (c.R * 255),
                        (byte) (c.G * 255),
                        (byte) (c.B * 255)
                        );
                    bitmap.SetPixel(i, j, col);
                }
            }
            bitmap.Save("result.png");
        }
    }
}