using Gtk;
using Gdk;
using System;
using System.Text;
using ContainerValidatorApp;

#pragma warning disable CS0612, CS0618 // Disable obsolete warnings for GTK# compatibility

namespace MyConsoleApp.UI.Tabs
{
    public class ValidatorTab : Box
    {
        private readonly Entry _entry;
        private readonly Label _resultLabel;
        private readonly Button _validateButton;

        public ValidatorTab() : base(Orientation.Vertical, 4)
        {
            BorderWidth = 6;
            Spacing = 4;

            // Outer scroll wrapper
            var outerScroll = new ScrolledWindow
            {
                ShadowType = ShadowType.None,
                HscrollbarPolicy = PolicyType.Never,
                VscrollbarPolicy = PolicyType.Automatic
            };

            var content = new Box(Orientation.Vertical, 4);
            outerScroll.Add(content);
            PackStart(outerScroll, true, true, 0);

            // Tiny header
            content.PackStart(new Label
            {
                Markup = "<span size='small' weight='bold'>Container Check</span>",
                Xalign = 0f
            }, false, false, 2);

            // Minimal hint
            content.PackStart(new Label
            {
                Markup = "<span size='xx-small' color='#888'>Enter / drop</span>",
                Xalign = 0f,
                Wrap = true
            }, false, false, 0);

            // Very compact input row
            _entry = new Entry
            {
                PlaceholderText = "ABCD1234567",
                WidthRequest = 160,
                HeightRequest = 24
            };
            _entry.ModifyFont(Pango.FontDescription.FromString("Monospace 10"));

            var targets = new[] { new TargetEntry("STRING", TargetFlags.App, 0) };
            Gtk.Drag.DestSet(_entry, DestDefaults.All, targets, DragAction.Copy);
            _entry.DragDataReceived += OnDragDataReceived;

            _validateButton = new Button("Check")
            {
                WidthRequest = 60,
                HeightRequest = 24
            };
            _validateButton.Clicked += OnValidateClicked;

            var inputRow = new Box(Orientation.Horizontal, 4);
            inputRow.PackStart(_entry, true, true, 0);
            inputRow.PackStart(_validateButton, false, false, 0);
            content.PackStart(inputRow, false, false, 4);

            // Tiny result area
            _resultLabel = new Label
            {
                Xalign = 0f,
                Yalign = 0f,
                UseMarkup = true,
                Selectable = true,
                Wrap = true,
                Markup = "<span color='#888' size='xx-small'>Enter number…</span>"
            };

            var resultScroll = new ScrolledWindow
            {
                ShadowType = ShadowType.In,
                HscrollbarPolicy = PolicyType.Never,
                VscrollbarPolicy = PolicyType.Automatic
            };
            resultScroll.Add(_resultLabel);

            content.PackStart(resultScroll, true, true, 0);

            ShowAll();
        }

        private void OnDragDataReceived(object? o, DragDataReceivedArgs args)
        {
            if (args.SelectionData?.Length > 0)
            {
                var text = Encoding.UTF8.GetString(args.SelectionData.Data ?? Array.Empty<byte>()).Trim();
                _entry.Text = text;
                ValidateContainer(text);
            }
            Gtk.Drag.Finish(args.Context, true, false, args.Time);
            args.RetVal = true;
        }

        private void OnValidateClicked(object? sender, EventArgs e)
        {
            ValidateContainer(_entry.Text.Trim());
        }

        private void ValidateContainer(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                _resultLabel.Markup = "<span color='#888' size='xx-small'>Enter / drop number</span>";
                return;
            }

            var result = ContainerValidator.Validate(input);

            string icon = result.IsValid ? "✓" : "✗";
            string color = result.IsValid ? "#27ae60" : "#c0392b";
            string status = result.IsValid ? "VALID" : "INVALID";

            _resultLabel.Markup =
                $"<span foreground='{color}' weight='bold' size='x-small'>{icon} {status}</span>\n" +
                $"<span size='xx-small'>{result.Message.Replace("\n", " • ")}</span>";
        }
    }
}

#pragma warning restore CS0612, CS0618