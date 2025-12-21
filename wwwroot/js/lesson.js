function showContent(id, element) {
    // Ẩn tất cả phần nội dung
    const contents = document.querySelectorAll('.content');
    contents.forEach((c) => c.classList.remove('active'));

    // Hiện phần được chọn
    const selected = document.getElementById(id);
    if (selected) selected.classList.add('active');

    // Đổi màu tab menu (HTML lesson: .sidebar_menu, CSS lesson: .menu)
    const menuItems = document.querySelectorAll('.sidebar_menu a, .menu a');
    menuItems.forEach((item) => item.classList.remove('active-link'));
    if (element) element.classList.add('active-link');
}

document.addEventListener('DOMContentLoaded', () => {
    // Đảm bảo luôn có 1 tab + 1 content active
    const hasActiveContent = document.querySelector('.content.active');
    if (!hasActiveContent) {
        const firstContent = document.querySelector('.content');
        if (firstContent) firstContent.classList.add('active');
    }

    const hasActiveLink = document.querySelector('.sidebar_menu a.active-link, .menu a.active-link');
    if (!hasActiveLink) {
        const firstLink = document.querySelector('.sidebar_menu a, .menu a');
        if (firstLink) firstLink.classList.add('active-link');
    }
});