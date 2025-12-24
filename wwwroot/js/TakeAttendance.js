document.addEventListener("DOMContentLoaded", () => {
    const button = document.getElementById("attendanceButton");
    const div = document.getElementById("takeAttendanceDiv");

    // Nhấn nút ngôi nhà → hiện/ẩn div điểm danh
    button.addEventListener("click", () => {
        if(div.style.display === "none") {
            div.style.display = "block";
        } else {
            div.style.display = "none";
        }
    });

    // Điểm danh 7 ô
    const today = new Date().toISOString().split("T")[0];
    let lastCheckedDay = localStorage.getItem("lastCheckedDay") || 0;
    let lastCheckedDate = localStorage.getItem("lastCheckedDate") || "";

    if (lastCheckedDate !== today) {
        lastCheckedDay = 0;
        localStorage.setItem("lastCheckedDate", today);
        localStorage.setItem("lastCheckedDay", lastCheckedDay);
    }

    const days = div.querySelectorAll(".day");
    days.forEach(day => {
        const dayNumber = parseInt(day.dataset.day);

        if(dayNumber <= lastCheckedDay){
            day.classList.add("checked");
        }

        day.addEventListener("click", () => {
            if(dayNumber === parseInt(lastCheckedDay) + 1){
                day.classList.add("checked");
                day.style.transform = "scale(1.3)";
                setTimeout(() => day.style.transform = "scale(1)", 300);

                lastCheckedDay = dayNumber;
                localStorage.setItem("lastCheckedDay", lastCheckedDay);

                alert(`Bạn đã điểm danh ngày ${dayNumber} thành công!`);

                // Ẩn div điểm danh sau khi click
                div.style.display = "none";
            } else if(dayNumber <= lastCheckedDay){
                alert("Bạn đã điểm danh ngày này rồi!");
            } else {
                alert(`Bạn phải điểm danh từ ngày ${parseInt(lastCheckedDay)+1} trước!`);
            }
        });
    });
});
