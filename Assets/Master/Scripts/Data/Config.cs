using UnityEngine;

public class Config 
{
    // Text Tutorial
    public const string Step_0 = "";
    public const string Step_1 = "";
    public const string Step_2 = "";
    public const string Step_3 = "";

    // Text Popup
    public const string Text_InValid_Value = "Giá trị nhập vào không hợp lệ\r\n Hãy thử lại";

    // TextElectron
    public const string Text_InValid_Electron_Value = "Giá trị nhập vào đã vượt quá số electron tối đa của lớp";
    public const string Text_Atom = "Số lớp vỏ và số electron không phù hợp\r\n Vui lòng kiểm tra lại";
    public const string Text_Kernel = "+";

    // Audio text 
    public const string Text_Atommic_Number = "Số hiệu nguyên tử (ký hiệu là Z) là số proton có trong hạt nhân của một nguyên tử";
    public const string Text_Atomic_Mass = "Khối lượng nguyên tử (ký hiệu là A) là khối lượng của proton và neutron trong hạt nhân, tính theo đơn vị amu";
    public const string Text_Atomic_Name = "Tên nguyên tố là tên gọi riêng của nguyên tố hóa học, dùng để phân biệt các nguyên tố với nhau";
    public const string Text_Atomic_Symbol = "Ký hiệu hóa học là chữ viết tắt của tên nguyên tố (1 hoặc 2 chữ cái).Chữ cái đầu viết hoa, chữ thứ hai (nếu có) viết thường";

    // Path
    public static string StreamingAssetsPath => Application.streamingAssetsPath;
    public const string ElementsData = "ElementsData.json";
    public const string FullElementDetails = "FullElementDetails.json";


    // Text Button
    public const string Button_Yes = "Đồng ý";
    public const string Button_No = "Không";

    // Text Introduction
    public const string Text_Introduction = "Có 2 cách để tạo mô hình nguyên tử:\r\n\n-Nhập số lớp vỏ và số electron tối đa cho mỗi lớp thông qua ô InputField\r\n\n-Ngoài ra có thể click vào icon Electron để kéo và thả từng electron cho mỗi lớp\r\n";

    
    // Get text of introduction_steps
    public static string GetStepText(int index)
    {
        switch (index)
        {
            case 0: return Step_0;
            case 1: return Step_1;
            case 2: return Step_2;
            case 3: return Step_3;
            default: return Step_3;
        }
    }
}
