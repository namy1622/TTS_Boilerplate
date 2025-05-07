(function ($) {
    var _$statisticService = abp.services.app.statistics;
    let categoryChart;
    CategoryChar('pie') // mặc định ban đầu là biểu đồ tròn
    //----------------------------------------------------------------------

    //== LẤY TỔNG VÀ TRUNG BÌNH DOANH THU
    _$statisticService.totalRevenueProduct()
        .done(function (result) {
            var formattedAmount = result.totalAmount.toLocaleString('vi-VN', {style:'currency',currency:'VND'}); 
                $('#totalRevenue').text(formattedAmount);
            var formattedQuantity = result.quantityProduct.toLocaleString('vi-VN');
                $('#totalQuantity').text(formattedQuantity);
            var averageAmount = parseInt(result.totalAmount) / parseInt(result.quantityProduct);
            var formattedAverage = averageAmount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                $('#averageOrderValue').text(formattedAverage);
        })
        .fail(function () {
            console.log('Lỗi lấy dữ liệu tổng doanh thu sản phẩm');
        })


    //-- LẤY DL CATEGORY TỪ SERVICE -- 
    function CategoryChar(selectedType) {
        _$statisticService.getDataForChartCategory()
            .done(function (result) {
                console.log(result);

                const labels = result.items.map(item => item.nameCategory);
                const quantityProducts = result.items.map(item => item.quantityProduct)

                drawCategoryChart(labels, quantityProducts, selectedType);
            })
            .fail(function () {
                console.log('Lôi  lấy dữ liêu!!!')
            });

    }
    function drawCategoryChart(labels, quantityProduct, selectedType) {
        var ctx = $('#productInCategoryChart')[0].getContext('2d');

        if (categoryChart) {
            categoryChart.destroy(); // hủy biểu đồ cũ trước khi vẽ lại
        }
        //-- màu random( trong trường hợp thêm loại mới thì ko cần vào code chỉnh màu)
        const colorsData = randomColor(labels);
        //---------------------------------------------------------------
        categoryChart = new Chart(ctx, {
            type: selectedType, //cột('bar'), đường('line'), tròn('pie')
            data: {
                labels: labels, // nhãn cho trục X
                datasets: [{
                    label: 'Số sản phẩm',
                    data: quantityProduct, // dữ liệu cho cột
                    // chỉnh màu, border
                    backgroundColor: colorsData.colors,
                    borderColor: colorsData.borderColors,
                    borderWidth: 1
                }]
            },
            options: {
                //responsive: true,
                //maintainAspectRatio: false // nếu bạn  điều chỉnh tỉ lệ khung
            }
        })
    }

    //== LẤY TỔNG DOANH THU MỖI LOẠI SẢN PHẨM ==    
    _$statisticService.totalAmountOfCategory()
        .done(function (result) {
            console.log('-- tổng doanh thu loại hàng', result);

            const labels = result.map(item => item.nameCategory);
            const totalAmount = result.map(item => item.totalAmount);

            drawTotalAmountCategory(labels, totalAmount);
        })

    function drawTotalAmountCategory(labels, totalAmount) {
        var ctx = $('#totalamountforCatgoryChart')[0].getContext('2d');
        const colorsData = randomColor(labels);
        const totalAmountCategoryChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Tổng doanh thu',
                    data: totalAmount,
                    backgroundColor: colorsData.colors,
                    borderColor: colorsData.borderColors,
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false // nếu  muốn điều chỉnh tỉ lệ khung
            }
        })
    }

    // == HÀM LẤY MÀU NGẪU NHIÊN CHO BIỂU ĐỒ ==
    function randomColor(labels) {
        const colors = labels.map(() => {
            const r = Math.floor(Math.random() * 255);
            const g = Math.floor(Math.random() * 255);
            const b = Math.floor(Math.random() * 255);
            return `rgba(${r}, ${g}, ${b}, 0.7)`;
        });

        const borderColors = colors.map(color => color.replace('0.7', '1'));

        return { colors, borderColors };
    }

    // == ĐỔI LOẠI BIỂU ĐỒ ==
    $('#chartType').change(function () {
        var selectedType = $(this).val();
        CategoryChar(selectedType);

    })

})(jQuery);