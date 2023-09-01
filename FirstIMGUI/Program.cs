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


        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        bool checkBoxValue = false;
        int Meter = 0;
        string input = "";
        string input2 = "";
        Vector4 selectedColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        protected override void Render()
        {

            ImGuiStylePtr style = ImGui.GetStyle();
            style.WindowBorderSize = 2.0f;
            style.Colors[(int)ImGuiCol.Border] = selectedColor;
            style.Colors[(int)ImGuiCol.TitleBgActive] = selectedColor;

            ImGui.Begin("New Window");
            ImGui.Text("Hello Text");
            ImGui.SliderInt("This is a slider", ref Meter, 1,100);
            ImGui.Checkbox("Check Me", ref checkBoxValue);

            ImGui.BeginChild("Child Window", new Vector2(300,200));
            ImGui.Text("Child Window text");
            ImGui.InputText("input", ref input, 16);
            ImGui.ColorPicker4("Selected Color", ref selectedColor);
            ImGui.EndChild();
            ImGui.SameLine();

            ImGui.BeginChild("Child2 Window", new Vector2(300, 200));
            ImGui.Text("Child2 Window text");
            ImGui.InputText("input2", ref input2, 16);
            ImGui.EndChild();
            ImGui.End();

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