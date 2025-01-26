using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PathFollower))]
class EditPathCtrl : Editor{
    PathFollower PathFollower;
    int CurrentIndex = -1; // Địa chỉ điểm đang giữ trong List MovePoint
    void OnEnable() // Được gọi mỗi khi Object được chọn
    {
        PathFollower = target as PathFollower; // Lấy tham chiếu đến PathFollower đang được chọn
    }
    void OnSceneGUI() // Được gọi mỗi khi có bất kỳ thay đổi nào ở GUI
    {
        if (PathFollower.Editing)
        {
            HandleEvents();
            HandleUI();
        }
    }
    void HandleUI() // xử lý giao diện 
    {
        Handles.BeginGUI(); // Vẽ layout hiển thị đang chế độ xoá hay thêm điểm
        {   
            GUILayout.BeginArea(new Rect(50,30, 200, 200));
            {
                if (Event.current.modifiers == EventModifiers.Control){
                    GUILayout.Label("Removing points");
                }else{
                    GUILayout.Label("Adding points,hold Ctrl to remove");
                }
            }
            GUILayout.EndArea();
        }
        Handles.EndGUI();
    }
    void HandleEvents() // Xử lý các sự kiện khi ấn chuột
    {
        Event CurrentEvent = Event.current;
        if(CurrentEvent.type == EventType.Repaint)  // Gọi hàm draw nếu Unity gọi sự kiện Repaint
        {
            Draw();
        }
        // Tạo 1 điều khiển cho GUI và gán cho điều khiển này là Passive , khi ấn vào 1 khoảng không sẽ ko đổi Object đang chọn
        // Khi ấn chuột ra ngoài đối tượng đang chọn Unity kiểm tra điều khiển hiện tại để xác định có control nào đang được chọn hay ko
        // Nếu ko có điều khiển nào đăng ký thì Unity sẽ bỏ chọn đối tượng
        // Vì được thêm 1 control với focusType.Passive nghĩa là control này ko nghe sự kiện từ người dùng và khi ấn chuột thì điều khiển này vẫn nhận event mà ko bị đổi đối tượng
        if(CurrentEvent.type== EventType.Layout)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
        // Chuyển đổi toạ độ chuột từ GUI sang WorldSpace
         
        if(CurrentEvent.type==EventType.MouseDown && CurrentEvent.button==0 && CurrentEvent.modifiers!=EventModifiers.Control){
            Vector2 MousePos = HandleUtility.GUIPointToWorldRay(CurrentEvent.mousePosition).origin;
            HandleLeftMouseDownAdd(MousePos);
        }else if (CurrentEvent.type == EventType.MouseDown && CurrentEvent.button==0 && CurrentEvent.modifiers == EventModifiers.Control){
            Vector2 MousePos = HandleUtility.GUIPointToWorldRay(CurrentEvent.mousePosition).origin;
            HandleLeftMouseDownDelete(MousePos);
        }else if(CurrentEvent.type == EventType.MouseDrag && CurrentEvent.button==0 && CurrentEvent.modifiers == EventModifiers.None){
            Vector2 MousePos = HandleUtility.GUIPointToWorldRay(CurrentEvent.mousePosition).origin;
            HandleLeftMouseDrag(MousePos);
        }else if(CurrentEvent.type == EventType.MouseUp && CurrentEvent.button==0 && CurrentEvent.modifiers == 0)
        {
            HandleLeftMouseUp();
        }
    }
    void Draw() // Vẽ đường nối các điểm , các chấm tại các điểm
    {
        Handles.color = Color.red;
        for (int i = 0; i < PathFollower.WayPoint.Count - 1; i++)
        {
            Handles.DrawLine(PathFollower.WayPoint[i], PathFollower.WayPoint[i + 1]);
            Handles.color = Color.white;
            Handles.DrawWireDisc(PathFollower.WayPoint[i], Vector3.forward, PathFollower.handleRadius);
            Handles.Label(PathFollower.WayPoint[i] + new Vector2(0.2f, 0.2f), i.ToString(), EditorStyles.boldLabel);
        }

        if (PathFollower.loop && PathFollower.WayPoint.Count > 1)
        {
            Handles.color = Color.red;
            Handles.DrawLine(PathFollower.WayPoint[^1], PathFollower.WayPoint[0]);
        }
        Handles.color = Color.white;
        Handles.DrawWireDisc(PathFollower.WayPoint[^1], Vector3.forward, PathFollower.handleRadius);
        Handles.Label(PathFollower.WayPoint[^1] + new Vector2(0.2f, 0.2f), (PathFollower.WayPoint.Count - 1).ToString(), EditorStyles.boldLabel);
    }
    void HandleLeftMouseDownDelete(Vector2 MousePos) // Xử lý sự kiện xoá 1 điểm
    {
        for (int i=0; i< PathFollower.WayPoint.Count ; i++){
            if(Vector2.Distance(PathFollower.WayPoint[i],MousePos) <= PathFollower.handleRadius){
                PathFollower.WayPoint.RemoveAt(i);
                HandleUtility.Repaint(); // Yêu cầu UNity vẽ lại khi ấn chuột;
                // Nhắc Unity là pathFollower này đã bị thay đổi vì khi chỉnh trong Editor các tuỳ chỉnh này sẽ chưa được lưu
                EditorUtility.SetDirty(PathFollower); 
                return;
            }
        }
    }
    void HandleLeftMouseDownAdd(Vector2 MousePos) // Xử lý sự kiện thêm 1 điểm nếu ko thấy điểm nào khi ấn chuột
    {
        
        for (int i=0; i< PathFollower.WayPoint.Count ; i++){
            if(Vector2.Distance(PathFollower.WayPoint[i],MousePos) <= PathFollower.handleRadius){
                CurrentIndex=i;
                return; // dừng duyệt khi đã tìm tháy điểm
            }
        }
        PathFollower.WayPoint.Add(MousePos); // Nếu ko chọn điểm nào thì thêm điểm mới
        CurrentIndex = PathFollower.WayPoint.Count-1;
        HandleUtility.Repaint(); // Yêu cầu UNity vẽ lại khi ấn chuột;
        // Nhắc Unity là pathFollower này đã bị thay đổi vì khi chỉnh trong Editor các tuỳ chỉnh này sẽ chưa được lưu
        EditorUtility.SetDirty(PathFollower);
    }
    void HandleLeftMouseDrag(Vector2 mousePos) // Xử lý sự kiện kéo điểm
    {
        PathFollower.WayPoint[CurrentIndex] = mousePos;
        HandleUtility.Repaint(); 
        // Nhắc Unity là pathFollower này đã bị thay đổi vì khi chỉnh trong Editor các tuỳ chỉnh này sẽ chưa được lưu
        EditorUtility.SetDirty(PathFollower);
    }
    void HandleLeftMouseUp() // Reset địa chỉ về -1 khi thả chuột
    {
        CurrentIndex = -1; // Reset CurrentIndex= - 1 là ko chọn điểm nào khi mouseUp
    }
}