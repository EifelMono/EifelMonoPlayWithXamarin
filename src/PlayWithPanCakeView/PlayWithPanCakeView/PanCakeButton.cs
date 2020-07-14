using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace PlayWithPanCakeView
{
public class PanCakeButton : PancakeView
    {
        public static BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(Button));
        public static BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(Button));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public double AnimationScale { get; set; } = 0.9;

        public PanCakeButton()
        {
            Padding = new Thickness(10);
            BackgroundColor = Color.FromHex("#388E3C");
            Shadow = new DropShadow()
            {
                Offset = new Point(3, 3),
                BlurRadius = 5,
                Color = Color.White
            };

            var tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += TapRecognizer_Tapped;

            GestureRecognizers.Add(tapRecognizer);
        }

        private async void TapRecognizer_Tapped(object sender, EventArgs e)
        {
            var view = (View)sender;

            await view.ScaleTo(AnimationScale);

            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }

            await view.ScaleTo(1.0);
        }
    }
}
