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
});

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

function checkCompleted(section) {
    const form = document.getElementById(`form-${section}`);
    const msg = document.getElementById(`msg-${section}`);

    let answered = 0;

    for (let i = 1; i <= 4; i++) {
        const checked = form.querySelector(`input[name="q${i}"]:checked`);
        if (checked) answered++;
    }

    if (answered < 4) {
        msg.style.color = "red";
        msg.innerHTML = "⚠️ Vui lòng trả lời đầy đủ 4 câu hỏi!";
    } else {
        msg.style.color = "green";
        msg.innerHTML = "✅ Bạn đã hoàn thành bài test!";
    }
}
function checkTest(section) {
    const testBox = document.getElementById(`test-${section}`);
    const questions = testBox.querySelectorAll(".question");
    const msg = document.getElementById(`msg-${section}`);
    const resetBtn = testBox.querySelector(".btn-reset");

    let correct = 0;

    questions.forEach(q => {
        const right = q.dataset.answer;
        const checked = q.querySelector("input:checked");
        const result = q.querySelector(".result");

        if (!checked) {
            result.textContent = "⚠ Chưa chọn";
            result.className = "result wrong";
            return;
        }

        if (checked.value === right) {
            result.textContent = "✔ Đúng";
            result.className = "result correct";
            correct++;
        } else {
            result.textContent = "✘ Sai";
            result.className = "result wrong";
        }
    });

    if (correct === questions.length) {
        msg.textContent = "🎉 Bạn đã hoàn thành bài học!";
        msg.style.color = "#16a34a";
        resetBtn.style.display = "none";
    } else {
        msg.textContent = `❌ Bạn đúng ${correct}/${questions.length} câu. Hãy làm lại.`;
        msg.style.color = "#dc2626";
        resetBtn.style.display = "inline-block";
    }
}

function resetTest(section) {
    const testBox = document.getElementById(`test-${section}`);

    testBox.querySelectorAll("input[type=radio]").forEach(r => r.checked = false);
    testBox.querySelectorAll(".result").forEach(r => r.textContent = "");
    testBox.querySelector(`#msg-${section}`).textContent = "";
    testBox.querySelector(".btn-reset").style.display = "none";
}
