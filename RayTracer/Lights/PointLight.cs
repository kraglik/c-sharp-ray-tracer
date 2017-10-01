namespace RayTracer.Lights {
    public class PointLight: Light {
        public Vector Position;
        public double Power;
        public double Radius;

        public PointLight(
                Vector position, 
                Color color, 
                double power = 1.0, 
                double radius = 1.0) : base(color) {
            Position = position;
            Power = power;
            Radius = radius;
        }

        public override double PowerAt(Vector pos) {
            var distance = (Position - pos).Len() / Radius;
            return distance > 1.0 ? 0.0: System.Math.Pow((Position - pos).Len() / Radius, Power);
        }

        public override Vector GetDirAt(Vector pos) {
            return (Position - pos).Normalized();
        }
        
        public override Color shade(Intersection intersection) {
            
            var direction = Position - intersection.Point;
            var ray = new Ray(intersection.Point, direction.Normalized());
            var intersections = intersection.Tracer.Scene.Intersections(ray);
            var result = new Color() + Color;
            var distance = direction.Len();
            foreach (var hit in intersections) {
                if (hit.Distance < distance)
                    result = result * (hit.Material.RefractiveColor * hit.Material.RefractivePower);
            }
            return result;
        }
    }
}