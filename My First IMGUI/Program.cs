using ImGuiNET;
using ClickableTransparentOverlay;
using System.Numerics;
using System.Runtime.InteropServices;

namespace IMGUITEST
{
    public class Program : Overlay
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);


        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        bool checkBoxValue = false;
        int Meter = 0;
        string input = "";
        string input2 = "";
        int tab = 0;

        bool showWindow = true;

        Vector4 selectedColor = new Vector4(0.13333333333333333f, 0.11372549019607843f, 0.7098039215686275f, 255f);

        protected override void Render()
        {
            //key is insert atm
            //https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
            if (GetAsyncKeyState(0x2D)<0)
            {
                showWindow = !showWindow;
                Thread.Sleep(200);
            }
            //^ you usually want this in your app logic and not in render logic
            if (showWindow) 
            { 
                ImGuiStylePtr style = ImGui.GetStyle();
                style.WindowBorderSize = 2.0f;
                style.Colors[(int)ImGuiCol.Border] = selectedColor;
                style.Colors[(int)ImGuiCol.TitleBgActive] = selectedColor;

                ImGui.Begin("New Window");
                ImGui.SetCursorPos(new Vector2(10, 20));
                if(ImGui.Button("Tab 0"))
                {
                    tab = 0;
                }
               ImGui.SetCursorPos(new Vector2(70, 20));
                if(ImGui.Button("Tab 1"))
                {
                    tab = 1;
                }
                ImGui.SetCursorPos(new Vector2(ImGui.GetWindowWidth() - 50, 20));
                if (ImGui.Button("Exit"))
                {
                    Environment.Exit(0);
                }
                if (tab == 0)
                {               
                    ImGui.Text("Hello Text");
                    ImGui.SliderInt("This is a slider", ref Meter, 1, 100);
                    ImGui.Checkbox("Check Me", ref checkBoxValue);

                    ImGui.BeginChild("Child Window", new Vector2(300, 200));
                    ImGui.Text("Child Window text");
                    ImGui.InputText("input", ref input, 16);

                    ImGui.BeginChild("Child2 Window", new Vector2(300, 200));
                    ImGui.Text("Child2 Window text");
                    ImGui.InputText("input2", ref input2, 16);
                    ImGui.EndChild();
                    ImGui.End();


                }
                if (tab == 1)
                {
                    ImGui.BeginChild("Color Selector", ImGui.GetWindowSize());
                    //TODO: make slsector fit whatever size the window is
                    ImGui.ColorPicker4("Selected Color", ref selectedColor);
                    ImGui.EndChild();
                    ImGui.SameLine();
                }

            }

        }
        public static void Main(string[] args)
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE); //hides console
            Console.WriteLine("starting");
            Program program = new Program();

            program.Start().Wait();

        }
    }
}