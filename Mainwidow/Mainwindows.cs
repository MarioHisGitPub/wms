using Gtk;
using Gdk;
using System;
using System.Text;
using System.Linq;
using MyConsoleApp.UI.Panels;
using MyConsoleApp.UI.Tabs;

#pragma warning disable CS0612, CS0618 // Disable obsolete warnings for GTK# compatibility

namespace MyConsoleApp.ui
{
    public class MainWindow : Gtk.Window
    {
        private Box _mainVBox = null!;
        private Box _buttonBar = null!;
        private Fixed _desktopArea = null!;
        private PanelManager _panelManager = null!;

        // Steam color palette
        private readonly Color SteamDarkBg = new Color(27, 40, 56);
        private readonly Color SteamMediumBg = new Color(38, 50, 66);
        private readonly Color SteamButtonBg = new Color(52, 73, 94);
        private readonly Color SteamBlue = new Color(102, 192, 244);
        private readonly Color SteamLightText = new Color(220, 221, 222);
        private readonly Color SteamWhite = new Color(255, 255, 255);

        private readonly string[] _models =
        {
            "Inventory", "Products", "Orders", "Locations",
            "Picking", "Dispatch", "Receiving", "Adjustments",
            "Suppliers", "Returns", "Batches", "Users", "Reports",
            "Customers", "Customer Invoices", "Supplier Invoices", "Validator"
        };

        public MainWindow() : base("Warehouse Management System")
        {
            // Adaptive size
            var screen = Gdk.Display.Default.DefaultScreen;
            int screenW = screen.Width;
            int screenH = screen.Height;
            int margin = 80;
            int maxW = screenW - (2 * margin);
            int maxH = screenH - (2 * margin);
            int w = Math.Min(1400, maxW);
            int h = Math.Min(900, maxH);
            w = Math.Max(1024, w);
            h = Math.Max(600, h);

            SetDefaultSize(w, h);
            SetSizeRequest(w, h);
            Resizable = true;
            WindowPosition = WindowPosition.Center;
            BorderWidth = 0;

            // Set window background - THIS IS CRITICAL
            SetBackgroundColor(this, SteamDarkBg);

            DeleteEvent += (o, args) => Application.Quit();

            BuildUI();
            ShowAll();
        }

        private void BuildUI()
        {
            _mainVBox = new Box(Orientation.Vertical, 0)
            {
                Hexpand = true,
                Vexpand = true
            };
            SetBackgroundColor(_mainVBox, SteamDarkBg);
            Add(_mainVBox);

            // HEADER
            var headerBox = new Box(Orientation.Horizontal, 15) { BorderWidth = 15 };
            SetBackgroundColor(headerBox, SteamDarkBg);

            // Logo
            AddLogo(headerBox);

            // Title
            var titleBox = new Box(Orientation.Vertical, 5);
            SetBackgroundColor(titleBox, SteamDarkBg);
            
            var titleLabel = new Label("Warehouse Management System") { Xalign = 0f };
            titleLabel.ModifyFont(Pango.FontDescription.FromString("Arial Bold 20"));
            titleLabel.ModifyFg(StateType.Normal, SteamWhite);
            
            var subtitleLabel = new Label("Manage your inventory, orders, and logistics efficiently") { Xalign = 0f };
            subtitleLabel.ModifyFont(Pango.FontDescription.FromString("Arial 10"));
            subtitleLabel.ModifyFg(StateType.Normal, SteamLightText);
            
            titleBox.PackStart(titleLabel, false, false, 0);
            titleBox.PackStart(subtitleLabel, false, false, 0);
            headerBox.PackStart(titleBox, true, true, 0);

            _mainVBox.PackStart(headerBox, false, false, 0);

            // Separator
            var separator = new EventBox();
            SetBackgroundColor(separator, SteamBlue);
            separator.SetSizeRequest(-1, 2);
            _mainVBox.PackStart(separator, false, false, 0);

            // BUTTON BAR
            CreateButtonBar();

            // DESKTOP AREA
            CreateDesktopArea();
        }

        private void AddLogo(Box headerBox)
        {
            try
            {
                var logoImage = new Image("/home/mario/Documents/app/csharp/wms/UI/logo.png");
                logoImage.SetSizeRequest(80, 80);
                headerBox.PackStart(logoImage, false, false, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading logo: {ex.Message}");
                var logoBox = new EventBox();
                SetBackgroundColor(logoBox, SteamBlue);
                logoBox.SetSizeRequest(80, 80);
                
                var logoLabel = new Label("WMS");
                logoLabel.ModifyFg(StateType.Normal, SteamWhite);
                logoLabel.ModifyFont(Pango.FontDescription.FromString("Arial Bold 24"));
                logoBox.Add(logoLabel);
                
                headerBox.PackStart(logoBox, false, false, 0);
            }
        }

        private void CreateButtonBar()
        {
            var buttonScrolled = new ScrolledWindow
            {
                HscrollbarPolicy = PolicyType.Automatic,
                VscrollbarPolicy = PolicyType.Never
            };
            SetBackgroundColor(buttonScrolled, SteamMediumBg);

            _buttonBar = new Box(Orientation.Horizontal, 5) { BorderWidth = 10 };
            SetBackgroundColor(_buttonBar, SteamMediumBg);
            buttonScrolled.Add(_buttonBar);
            _mainVBox.PackStart(buttonScrolled, false, false, 0);

            TargetEntry[] buttonTargets = { new TargetEntry("STRING", TargetFlags.App, 0) };

            foreach (var model in _models)
            {
                var btn = CreateDraggableButton(model, buttonTargets);
                _buttonBar.PackStart(btn, false, false, 0);
            }
        }

        private EventBox CreateDraggableButton(string modelName, TargetEntry[] buttonTargets)
        {
            var btnContainer = new EventBox();
            btnContainer.ModifyBg(StateType.Normal, new Color(45, 55, 70));
            btnContainer.ModifyBg(StateType.Prelight, SteamBlue);
            btnContainer.BorderWidth = 1;

            var btn = new Button(modelName) { Relief = ReliefStyle.None };
            btn.ModifyBg(StateType.Normal, new Color(45, 55, 70));
            btn.ModifyFg(StateType.Normal, SteamLightText);
            btn.ModifyBg(StateType.Prelight, SteamBlue);
            btn.ModifyFg(StateType.Prelight, SteamWhite);

            var label = (Label)btn.Child;
            label.ModifyFg(StateType.Normal, SteamLightText);
            label.ModifyFg(StateType.Prelight, SteamWhite);

            btnContainer.Add(btn);

            Gtk.Drag.SourceSet(btn, ModifierType.Button1Mask, buttonTargets, DragAction.Copy);
            btn.DragDataGet += (o, args) =>
            {
                byte[] data = Encoding.UTF8.GetBytes(modelName);
                args.SelectionData.Set(Gdk.Atom.Intern("STRING", false), 8, data);
            };

            return btnContainer;
        }

        private void CreateDesktopArea()
        {
            var desktopScrolled = new ScrolledWindow
            {
                HscrollbarPolicy = PolicyType.Automatic,
                VscrollbarPolicy = PolicyType.Automatic,
                ShadowType = ShadowType.None,
                Hexpand = true,
                Vexpand = true
            };
            SetBackgroundColor(desktopScrolled, SteamDarkBg);

            // Wrapper to ensure background paints
            var desktopWrapper = new EventBox 
            { 
                VisibleWindow = true,
                Hexpand = true,
                Vexpand = true
            };
            SetBackgroundColor(desktopWrapper, SteamDarkBg);

            _desktopArea = new Fixed
            {
                Hexpand = true,
                Vexpand = true
            };
            _desktopArea.SetSizeRequest(3000, 3000);
            SetBackgroundColor(_desktopArea, SteamDarkBg);

            desktopWrapper.Add(_desktopArea);
            
            // Create viewport manually with expand properties
            var viewport = new Viewport
            {
                ShadowType = ShadowType.None,
                Hexpand = true,
                Vexpand = true
            };
            SetBackgroundColor(viewport, SteamDarkBg);
            viewport.Add(desktopWrapper);
            
            desktopScrolled.Add(viewport);

            _mainVBox.PackStart(desktopScrolled, true, true, 0);

            _panelManager = new PanelManager(_desktopArea);

            // **SHOW DASHBOARD TAB ON STARTUP**
            _panelManager.CreateFloatingPanel("Dashboard", 50, 50);

            // Enable drop on desktop
            SetupDropTarget();
        }

        private void SetBackgroundColor(Widget widget, Color color)
        {
            // Set all possible states including BACKDROP - this was the missing piece!
            widget.ModifyBg(StateType.Normal, color);
            widget.ModifyBg(StateType.Active, color);
            widget.ModifyBg(StateType.Prelight, color);
            widget.ModifyBg(StateType.Selected, color);
            widget.ModifyBg(StateType.Insensitive, color);
            
            // Try to set backdrop state via style context (GTK3 way)
            try
            {
                var css = $@"
                * {{
                    background-color: rgb({color.Red / 256}, {color.Green / 256}, {color.Blue / 256});
                }}
                *:backdrop {{
                    background-color: rgb({color.Red / 256}, {color.Green / 256}, {color.Blue / 256});
                }}";
                
                var provider = new CssProvider();
                provider.LoadFromData(css);
                widget.StyleContext.AddProvider(provider, 600);
            }
            catch
            {
                // If CSS approach fails, continue with just ModifyBg
            }
        }

        private void SetupDropTarget()
        {
            TargetEntry[] dropTargets = { new TargetEntry("STRING", TargetFlags.App, 0) };
            Gtk.Drag.DestSet(_desktopArea, DestDefaults.All, dropTargets, DragAction.Copy);
            
            _desktopArea.DragDataReceived += (o, args) =>
            {
                string modelName = Encoding.UTF8.GetString(args.SelectionData.Data ?? Array.Empty<byte>());
                _panelManager.CreateFloatingPanel(modelName, (int)args.X, (int)args.Y);
                Gtk.Drag.Finish(args.Context, true, false, args.Time);
                args.RetVal = true;
            };
        }
    }
}

#pragma warning restore CS0612, CS0618
