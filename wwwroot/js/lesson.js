    function showContent(id, element) {
    // Ẩn tất cả phần nội dung
    const contents = document.querySelectorAll('.content');
    contents.forEach(c => c.classList.remove('active'));

    // Hiện phần được chọn
    const selected = document.getElementById(id);
    if (selected) selected.classList.add('active');

    // Đổi màu tab menu
    const menuItems = document.querySelectorAll('.sidebar_menu a');
    menuItems.forEach(item => item.classList.remove('active-link'));
    element.classList.add('active-link');
    }