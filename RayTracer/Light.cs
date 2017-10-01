namespace RayTracer {
    public abstract class Light {
        public Color Color;

        protected Light(Color color) {
            Color = color;
        }

        public abstract Vector GetDirAt(Vector pos);

        public abstract double PowerAt(Vector pos);

        public abstract Color shade(Intersection intersection);
    }
}