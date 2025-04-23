kiểm tra _$productTable?
    _$productTable có thể là undefined 
    nếu bảng DataTables chưa được khởi tạo hoàn toàn hoặc bị lỗi
    Việc kiểm tra đảm bảo code không bị lỗi TypeError và cung cấp giá trị mặc định hợp lý.

-----------------------------------------------------------------------------------------------
var draw = _$productTable ? _$productTable.settings()[0].oAjaxData.draw : 1;
    - ktra _$ProductTable có tồn tại -> lấy gtri draw từ oAjaxData trong settings()[0]
    - draw là số nguyên tăng dần, giúp server biết yc tương ứng response nào
        draw được DataTable gửi kèm trong mọi yc ajax và được trả về trong response từ server để đảm bảo đồng bộ 
    - nếu _$productTable ko tồn tại -> mặc định 1 để đảm bảo yc vẫn được gửi 
--
var start = _$productTable ? _$productTable.settings()[0]._iDisplayStart : 0;
    -  lấy iDisplayStart tư trạng thái datatable, biểu thị chỉ số bản ghi đầu tiên của hiện tại 
            Ví dụ: Nếu trang 1 hiển thị 10 bản ghi, start = 0; trang 2 thì start = 10.
    - start tương ứng skipCount trong API server, giúp server biết cần bỏ qua số bản ghi trước khi lấy dữ liệu
    - _$productTable không tồn tại -> mặc định start = 0 (bắt đầu từ bản ghi đầu tiên)
--
var length = _$productTable ? _$productTable.settings()[0]._iDisplayLength : 10;
    - _iDisplayLength chỉ số bản ghi Tối Đa hiển thị trên MỖI TRANG
    - _$productTable không tồn tại -> mặc định length = 10
--
_$formObj:
    Đây là đối tượng chứa các tham số được gửi tới server

_$formObj.skipCount = start;
    skipCount là tham số API server(GetAllProductCategory) sd để bỏ qua các bản ghi trước khi lấy dữ liệu
    Nếu start = 10 -> server sẽ bỏ qua 10 bản ghi đầu tiên

_$formObj.maxResultCount = length;
    maxResultCount xác định server trả về bao nhiêu bản ghi
    Nếu length = 10 -> server trả về tối đa 10 bản ghi

var order = _$productTable ? _$productTable.order() : [[1, 'asc']];
    - _$productTable.order() -> một mảng chứa thông tin sắp xếp hiện tại của bảng
                                    (phần tử mảng dạng [columnIndex, direction])
    - DT cho pheps nhấp vào trường tiêu đề cột -> thay đổi sắp xếp || order() -> lấy trạng thái sx hiện tại

if (order && order.length > 0) { // order tồn tại, có ít nhất 1 phần tử(tt sắp xếp)
    var columnIndex = order[0][0];
        - lấy chỉ số cột được sx từ phần tử đầu tien của order

    var direction = order[0][1];
        - lấy hướng sx(asc || desc) từ pt đầu tiên của order 

    var columnName = _$productTable ? _$productTable.settings()[0].aoColumns[columnIndex].name : 1;
        - lấy tên cột(đn trong columnDefs) tương ứng columnIndex
        - aoColumns: mảng chứa tt cột, mỗi cột có tt name (tên cột)
               ex: aoClumns[columnIndex].name -> lấy cột columnIndex với name (tên cột)
        --> name trong columnDefs ánh xạ trực tiếp tên trường API server -> giúp server hiểu cột nào cần sx

    _$formObj.sorting = columnName + ' ' + direction;
} else {
    _$formObj.sorting = 'NameProduct asc'; // Mặc định
}