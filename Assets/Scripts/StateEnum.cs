public enum StateEnum //Đánh số cho các trạng thái
{
    Idle= 0, // Trạng thái nghỉ hoặc off của mọi Object
    Run = 1,
    Jump = 2,
    Fall =3,
    Climb=4,
    DoubleJump=5,
    On = 6 , // Trap kích hoạt
    DetectPlayer =7, // Phát hiện player
    SpecialState = 8, // Trạng thái đặc biệt của các đối tượng
    Appear = 9,
    DisAppear  = 10
}
