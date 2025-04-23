(function ($) {
  var _$idUser = abp.session.userId; // ✅ mở lại khai báo này
  var _$cartList = $('#cartList');
  //alert("1"); // test OK

  $(document).ready(function () {
    listCartItem.getCartItem();
  });

  var listCartItem = {

    getCartItem: function () {
      var self = this;
      var userId = abp.session.userId;
      //abp.ui.setBusy();
      abp.services.app.productService.get_ListCartItem( userId)  //({ userId: _$idUser })
      //abp.ajax({
      //  url: abp.appPath + 'Carts/IndexCarts?userId=' + _$idUser,
      //  type: 'GET',
      //  success: function (content) {
      //    if (!content) {
      //      console.error('Lỗi: Nội dung trả về trống!');
      //      return;
      //    }
      //  },
      //  error: function (e) {
      //    console.log('Looix Get CartItem', e);
      //  }
        .done(function (cartItems) {
          console.log('-- thành công Get in cart', cartItems);
          abp.notify.info("Lấy thành công!");

          self.renderCartItems(cartItems.items);
        })
        .fail(function (error) {
          console.error('Lỗi khi tải sản phẩm:', error);
          abp.notify.error('Lấy thất bại');
        })
        
        //.always(function () {
        //  abp.ui.clearBusy();
        //});
      },

        renderCartItems: function (cartItems) {

          console.log('-- cartItems', cartItems);
          if (!cartItems) {
            _$cartList.html('<div class="col-12 text-center">Không có sản phẩm nào</div>');
            return;
          }
          var lengthCartItem = cartItems.length;
          console.log('-- length cart', lengthCartItem);
          
          }

        }
  
  
})(jQuery);
