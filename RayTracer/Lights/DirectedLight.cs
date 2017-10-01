namespace RayTracer.Lights {
    public class DirectedLight: Light {
        public Vector Direction;

        public DirectedLight(Vector direction, Color color): base(color) {
            Direction = direction;
        }
        
        public override Vector GetDirAt(Vector pos) {
            return Direction;
        }

        public override double PowerAt(Vector pos) {
            return 1.0;
        }

        public override Color shade(Intersection intersection) {
            var ray = new Ray(intersection.Point, -Direction);
            var intersections = intersection.Tracer.Scene.Intersections(ray);
            var result = new Color() + Color;
            foreach (var hit in intersections) {
                result = result * (hit.Material.RefractiveColor * hit.Material.Refractivity);
            }
            return result;
        }
    }
}