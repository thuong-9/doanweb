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
<<<<<<< HEAD
document.addEventListener("DOMContentLoaded", function () {

    const overlay = document.getElementById("attendanceOverlay");
    const openBtn = document.getElementById("openAttendance");
    const closeBtn = document.getElementById("closeAttendance");
    const days = document.querySelectorAll(".day");

    const today = new Date().toISOString().slice(0, 10); // yyyy-mm-dd

    let lastCheckedDay = Number(localStorage.getItem("attendance_lastDay")) || 0;
    let lastDate = localStorage.getItem("attendance_lastDate");
    let checkedToday = lastDate === today;

    openBtn.onclick = () => overlay.style.display = "block";
    closeBtn.onclick = () => overlay.style.display = "none";

    // 🔒 tô màu + khóa các ngày đã điểm
    days.forEach(day => {
        const dayNumber = Number(day.dataset.day);
        if (dayNumber <= lastCheckedDay) {
            day.classList.add("checked");
            day.style.pointerEvents = "none";
        }
    });

    days.forEach(day => {
        const dayNumber = Number(day.dataset.day);

        day.onclick = () => {

            // ❌ hôm nay đã điểm
            if (checkedToday) {
                alert("Bạn đã điểm danh hôm nay!");
                return;
            }

            // ❌ sai thứ tự
            if (dayNumber !== lastCheckedDay + 1) {
                alert("Hãy lựa chọn lại thứ tự nhé!");
                return;
            }

            // ✅ điểm danh
            day.classList.add("checked");
            day.style.pointerEvents = "none";

            lastCheckedDay++;
            checkedToday = true;

            localStorage.setItem("attendance_lastDay", lastCheckedDay);
            localStorage.setItem("attendance_lastDate", today);

            // 🔒 khóa toàn bộ ô còn lại trong ngày
            days.forEach(d => d.style.pointerEvents = "none");

            // 🎉 ngày 7 nổ
            if (lastCheckedDay === 7) {
                day.classList.add("boom");
                setTimeout(() => {
                    alert("🎉 Hoàn thành chuỗi 7 ngày!");
                }, 400);
            }
        };
    });
=======

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
>>>>>>> d378627 (Initial commit)
});