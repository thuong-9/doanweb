/**
 * File: wwwroot/js/exercise-menu.js
 * Xử lý logic đóng/mở menu hamburger
 */
document.addEventListener('DOMContentLoaded', function () {
    const hamburger = document.getElementById('hamburger');
    const menu = document.getElementById('menu');

    if (hamburger) {
        hamburger.addEventListener('click', function (e) {
            // Toggle class active để biến 3 gạch thành X và hiện menu
            this.classList.toggle('active');
            
            // Ngăn sự kiện click lan ra ngoài (để không bị đóng ngay lập tức)
            e.stopPropagation();
        });
    }

    // Tự động đóng menu khi người dùng click vào bất kỳ đâu bên ngoài menu
    document.addEventListener('click', function (e) {
        if (hamburger && !hamburger.contains(e.target)) {
            hamburger.classList.remove('active');
        }
    });

    // Đóng menu khi nhấn phím Esc (tăng trải nghiệm người dùng)
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape' && hamburger) {
            hamburger.classList.remove('active');
        }
    });
});