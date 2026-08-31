using JoeTween;

namespace JoeTween
{
    public abstract class TweenReflectionNode<T> : TweenActionNode<T>
    {
        public string componentName;
        public string memberName;

        public T start;
        public T end;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort<string>("Component Name").Build();
            context.AddInputPort<string>("Member Name").Build();

            context.AddInputPort<T>("Start").Build();
            context.AddInputPort<T>("End").Build();
        }

        public override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Component Name").TryGetValue(out componentName);
            GetInputPortByName("Member Name").TryGetValue(out memberName);

            GetInputPortByName("Start").TryGetValue(out start);
            GetInputPortByName("End").TryGetValue(out end);
        }
    }

}
