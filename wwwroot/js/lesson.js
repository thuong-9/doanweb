//html-lesson
function showContent(id, element) {
    // 1. Ẩn tất cả nội dung
    const contents = document.querySelectorAll('.content');
    contents.forEach((c) => {
        c.classList.remove('active');
        c.style.display = 'none'; 
    });

    // 2. Hiện phần nội dung được chọn
    const selected = document.getElementById(id);
    if (selected) {
        selected.classList.add('active');
        selected.style.display = 'block';
    }

    // 3. Cập nhật trạng thái menu active
    const menuItems = document.querySelectorAll('.sidebar_menu a, .menu a');
    menuItems.forEach((item) => item.classList.remove('active-link'));
    if (element) element.classList.add('active-link');

    // 4. QUAN TRỌNG: Gọi vẽ câu hỏi và cập nhật % cho mục vừa chọn
    if (typeof renderQuiz === 'function') {
        renderQuiz(id);
    }
    if (typeof updateProgressUI === 'function') {
        updateProgressUI(id);
    }
}
//bai test
const quizData = {
    'gioithieu': [
        { q: "HTML dùng để làm gì trong trang web?", o: ["Xử lý logic chương trình", "Tạo cấu trúc cho trang web", "Thiết kế cơ sở dữ liệu", "Quản lý máy chủ"], c: 1 },
        { q: "HTML là ngôn ngữ thuộc loại nào?", o: ["Ngôn ngữ lập trình", "Ngôn ngữ đánh dấu", "Ngôn ngữ kịch bản", "Ngôn ngữ truy vấn"], c: 1 },
        { q: "Thành phần cơ bản nhất của HTML là gì?", o: ["Hàm", "Biến", "Thẻ (tag)", "Lớp (class)"], c: 2 },
        { q: "Trình duyệt web đọc HTML để làm gì?", o: ["Biên dịch chương trình", "Hiển thị nội dung trang web", "Lưu dữ liệu người dùng", "Chạy thuật toán"], c: 1 },
        { q: "HTML thường được kết hợp với CSS để làm gì?", o: ["Xử lý logic", "Tạo hiệu ứng động", "Trang trí giao diện", "Kết nối cơ sở dữ liệu"], c: 2 }
    ],
    'cautruc': [
        { q: "Thẻ nào được dùng để khai báo loại tài liệu HTML5?", o: ["<html>", "<!DOCTYPE html>", "<head>", "<meta>"], c: 1 },
        { q: "Phần nào của tài liệu HTML chứa nội dung hiển thị trên trình duyệt?", o: ["<head>", "<title>", "<html>", "<body>"], c: 3 },
        { q: "Thẻ <head> trong HTML dùng để làm gì?", o: ["Chứa nội dung chính của trang", "Chứa thông tin mô tả trang web", "Hiển thị hình ảnh", "Tạo liên kết"], c: 1 },
        { q: "Thẻ <title> có chức năng gì?", o: ["Hiển thị tiêu đề trong nội dung trang", "Xác định tiêu đề hiển thị trên tab trình duyệt", "Tạo tiêu đề lớn nhất trong trang", "Định dạng văn bản"], c: 1 },
        { q: "Trong cấu trúc HTML, thẻ nào là phần tử gốc (root element)?", o: ["<body>", "<head>", "<html>", "<!DOCTYPE html"], c: 2 }
    ],
    'soanthao': [
        { q: "Thẻ nào dùng để tạo đoạn văn trong HTML?", o: ["<div>", "<span>", "<p>", "<br>"], c: 2 },
        { q: "Thẻ nào dùng để xuống dòng nhưng không tạo đoạn mới?", o: ["<p>", "<hr>", "<br>", "<span>"], c: 2 },
        { q: "Thẻ nào dùng để tạo tiêu đề lớn nhất trong HTML?", o: ["<h6>", "<head>", "<title>", "<h1>"], c: 3 },
        { q: "Thẻ nào dùng để in đậm văn bản và có ý nghĩa ngữ nghĩa?", o: ["<b>", "<strong>", "<i>", "<em>"], c: 1 },
        { q: "Thẻ nào dùng để in nghiêng văn bản và mang ý nghĩa nhấn mạnh?", o: ["<i>", "<em>", "<b>", "<mark>"], c: 1 }
    ],
    'cacthe':[   
        { q: "Thẻ nào dùng để tạo liên kết (link) trong HTML?", o: ["<link>", "<a>", "<href>", "<url>"], c: 1 },
        { q: "Thẻ nào dùng để chèn hình ảnh vào trang web?", o: ["<image>", "<img>", "<picture>", "<src>"], c: 1 },
        { q: "Thẻ nào dùng để tạo danh sách không có thứ tự?", o: ["<ol>", "<li>", "<ul>", "<dl>"], c: 2 },
        { q: "Thẻ nào dùng để tạo danh sách có thứ tự?", o: ["<ul>", "<li>", "<ol>", "<menu>"], c: 2 },
        { q: "Thẻ nào dùng để tạo một khối chứa nội dung (block-level) trong HTML?", o: ["<span>", "<div>", "<section>", "<article>"], c: 1 }
    ],
    'thuoctinh':[
        { q: "Thuộc tính nào được khai báo trong thẻ <html> để xác định ngôn ngữ của tài liệu?", o: ["charset", "lang", "title", "type"], c: 1 },
        { q: "Thuộc tính nào dùng để chỉ đường dẫn liên kết trong thẻ <a>?", o: ["src", "link", "href", "url"], c: 2 },
        { q: "Thuộc tính nào dùng để chỉ đường dẫn hình ảnh trong thẻ <img>?", o: ["alt", "href", "src", "title"], c: 2 },
        { q: "Thuộc tính alt trong thẻ <img> có tác dụng gì?", o: ["Thay đổi kích thước ảnh", "Mô tả ảnh khi không hiển thị", "Liên kết ảnh", "Trang trí ảnh"], c: 1 },
        { q: "Thuộc tính nào dùng để khai báo kích thước chiều rộng và chiều cao của hình ảnh?", o: ["size", "scale", "width và height", "resize"], c: 2 }
    ],
    'lienket': [
        { q: "Thuộc tính nào được khai báo trong thẻ <html> để xác định ngôn ngữ của tài liệu?", o: ["charset", "lang", "title", "type"], c: 1 },
        { q: "Thuộc tính nào dùng để chỉ đường dẫn liên kết trong thẻ <a>?", o: ["src", "link", "href", "url"], c: 2 },
        { q: "Thuộc tính nào dùng để chỉ đường dẫn hình ảnh trong thẻ <img>?", o: ["alt", "href", "src", "title"], c: 2 },
        { q: "Thuộc tính alt trong thẻ <img> có tác dụng gì?", o: ["Thay đổi kích thước ảnh", "Mô tả ảnh khi không hiển thị", "Liên kết ảnh", "Trang trí ảnh"], c: 1 },
        { q: "Thuộc tính nào dùng để khai báo kích thước chiều rộng và chiều cao của hình ảnh?", o: ["size", "scale", "width và height", "resize"], c: 2 }
    ],
    'hinhanh': [
        { q: "Thẻ nào dùng để chèn hình ảnh vào trang HTML?", o: ["<picture>", "<image>", "<img>", "<media>"], c: 2 },
        { q: "Thuộc tính nào dùng để xác định đường dẫn tới hình ảnh?", o: ["href", "alt", "src", "link"], c: 2 },
        { q: "Thuộc tính alt trong thẻ <img> có vai trò gì?", o: ["Tạo hiệu ứng ảnh", "Mô tả ảnh khi không hiển thị", "Thay đổi kích thước ảnh", "Liên kết ảnh"], c: 1 },
        { q: "Thuộc tính nào dùng để thiết lập kích thước hiển thị của hình ảnh?", o: ["size", "scale", "width và height", "resize"], c: 2 },
        { q: "Khi ảnh không tải được, trình duyệt sẽ hiển thị nội dung của thuộc tính nào?", o: ["src", "alt", "title", "id"], c: 1 }
    ],
    'bangbieu' :[
        { q: "Thẻ nào dùng để khai báo một bảng trong HTML?", o: ["<tr>", "<td>", "<table>", "<th>"], c: 2 },
        { q: "Thẻ nào dùng để khai báo một hàng trong bảng?", o: ["<td>", "<tr>", "<th>", "<table>"], c: 1 },
        { q: "Thẻ nào dùng để khai báo ô tiêu đề trong bảng?", o: ["<td>", "<tr>", "<th>", "<table>"], c: 2 },
        { q: "Thuộc tính nào dùng để gộp nhiều cột trong bảng?", o: ["rowspan", "merge", "colspan", "span"], c: 2 },
        { q: "Thuộc tính rowspan dùng để gộp các ô theo hướng nào?", o: ["Theo cột", "Theo hàng", "Theo chiều ngang", "Theo chiều dọc"], c: 1 }
    ],
    'danhsach': [
        { q: "Thẻ nào dùng để tạo danh sách không có thứ tự trong HTML?", o: ["<ol>", "<ul>", "<li>", "<dl>"], c: 1 },
        { q: "Thẻ nào dùng để khai báo một phần tử trong danh sách?", o: ["<ul>", "<ol>", "<li>", "<dt>"], c: 2 },
        { q: "Thẻ nào dùng để tạo danh sách có thứ tự trong HTML?", o: ["<ul>", "<ol>", "<li>", "<dd>"], c: 1 },
        { q: "Thuộc tính nào dùng để thay đổi kiểu hiển thị của danh sách có thứ tự?", o: ["style", "class", "type", "order"], c: 2 },
        { q: "Trong danh sách định nghĩa, thẻ nào dùng để mô tả nội dung của thuật ngữ?", o: ["<dt>", "<dl>", "<dd>", "<li>"], c: 2 }
    ],
    'chuthich': [
        { q: "Cú pháp đúng để tạo chú thích trong HTML là gì?", o: ["// comment", "<!-- comment -->", "/* comment */", "<comment>"], c: 1 },
        { q: "Chú thích HTML có được hiển thị trên trình duyệt không?", o: ["Có", "Không", "Chỉ hiển thị khi inspect", "Chỉ hiển thị trong thẻ body"], c: 1 },
        { q: "Mục đích chính của chú thích trong HTML là gì?", o: ["Trang trí giao diện", "Ghi chú cho lập trình viên", "Tạo liên kết", "Tối ưu SEO"], c: 1 },
        { q: "Chú thích HTML thường được dùng khi nào?", o: ["Ẩn đoạn code", "Ghi giải thích code", "Cả A và B", "Tạo hiệu ứng"], c: 2 },
        { q: "Chú thích HTML có thể đặt ở đâu trong tài liệu?", o: ["Chỉ trong <head>", "Chỉ trong <body>", "Chỉ ngoài thẻ HTML", "Bất kỳ vị trí nào"], c: 3 }
    ]}

// Lưu vị trí câu hiện tại
let currentQuestionIndex = JSON.parse(localStorage.getItem('quiz_step')) || {};

function renderQuiz(id) {
    const container = document.getElementById(`quiz-area-${id}`);
    if (!container || !quizData[id]) return;

    const savedData = JSON.parse(localStorage.getItem('html_progress')) || {};
    const userAnswers = savedData[id] || {};
    const total = quizData[id].length;

    // --- KIỂM TRA TRẠNG THÁI HOÀN THÀNH ---
    // Nếu số lượng câu đã trả lời bằng tổng số câu, hiện màn hình hoàn thành
    if (Object.keys(userAnswers).length === total) {
        showCompletionScreen(id, container, userAnswers);
        return;
    }

    if (currentQuestionIndex[id] === undefined) currentQuestionIndex[id] = 0;
    const index = currentQuestionIndex[id];
    const item = quizData[id][index];
    const savedValue = userAnswers[index];

    let isAnswered = savedValue !== undefined;
    let html = `<h4>Kiểm tra kiến thức (${index + 1}/${total})</h4>`;
    let feedback = "";
html += `<div class="question-item ${isAnswered ? 'answered' : ''}" style="padding:20px; border-radius:10px; background:#fff; border-left: 5px solid #ddd;">
                <p><b>Câu ${index + 1}:</b> ${escapeHTML(item.q)}</p>
                <div id="options-${id}">`;

    item.o.forEach((opt, i) => {
        let labelClass = "opt-label";
        if (isAnswered) {
            if (i === item.c) labelClass += " correct-opt"; 
            else if (parseInt(savedValue) === i) labelClass += " incorrect-opt";
        }
        html += `<label class="${labelClass}" style="display:block; cursor:pointer; margin:8px 0; padding:10px; border:1px solid #eee; border-radius:5px; ${isAnswered ? 'pointer-events:none; opacity:0.8;' : ''}">
                    <input type="radio" name="q-${id}-${index}" value="${i}" ${savedValue == i ? 'checked' : ''} onchange="handleAnswer('${id}', ${index}, this.value)"> ${escapeHTML(opt)} </label>`;
    });

    html += `</div>`;
    if (isAnswered) {
        feedback = (parseInt(savedValue) === item.c) 
            ? `<p style="color:#04AA6D; font-weight:bold; margin-top:10px;">✔ Đúng rồi!</p>` 
            : `<p style="color:#f44336; font-weight:bold; margin-top:10px;">✘ Sai rồi! Đáp án đúng: ${item.o[item.c]}</p>`;
    }

    html += feedback;
    html += `<div style="margin-top:15px;">
                <button onclick="nextQuestion('${id}')" class="btn-check-section" style="display:${isAnswered ? 'block' : 'none'};">
                    ${index < total - 1 ? 'Tiếp theo' : 'Xem kết quả'}
                </button>
            </div></div>`;
    container.innerHTML = html;
}

// HÀM HIỂN THỊ MÀN HÌNH HOÀN THÀNH
function showCompletionScreen(id, container, userAnswers) {
    let correct = 0;
    const total = quizData[id].length;
    
    // 1. Tính số câu đúng
    quizData[id].forEach((item, i) => {
        if (parseInt(userAnswers[i]) === item.c) correct++;
    });

    const scorePercent = (correct / total) * 100;

    // 2. Lấy tên tiêu đề bài học từ thẻ h1 của section đó
    const sectionTitle = document.querySelector(`#${id} .main-title`)?.innerText || "bài học";

    let htmlContent = "";

    if (scorePercent ===100) {
        // TRƯỜNG HỢP đạt 100% - CHÚC MỪNG
        htmlContent = `
            <div class="khung" style="text-align:center; border: 2px solid #04AA6D; padding: 30px; border-radius: 15px; background: #f0fff4; animation: fadeIn 0.5s;">
                <h3 style="color:#04AA6D;">🎉 Tuyệt vời!</h3>
                <p style="font-size: 1.1em;">Bạn đã xuất sắc vượt qua bài tập phần: <br><b style="color:#2c3e50; font-size: 1.2em;">${sectionTitle}</b></p>
                <div style="margin: 20px 0;">
                    <span style="font-size: 2em; font-weight: bold; color: #04AA6D;">${correct}/${total}</span>
                    <p>Câu trả lời chính xác (${scorePercent}%)</p>
                </div>
            <button onclick="goToNextSection('${id}')" class="btn-check-section" style="background:#2196F3; margin-top: 10px;">Qua bài tiếp theo</button>            </div>`;
    } else {
        // TRƯỜNG HỢP DƯỚI 100% - YÊU CẦU LÀM LẠI
        htmlContent = `
            <div class="khung" style="text-align:center; border: 2px solid #f44336; padding: 30px; border-radius: 15px; background: #fff5f5; animation: shake 0.5s;">
                <h3 style="color:#f44336;">⚠️ Cố gắng lên!</h3>
                <p style="font-size: 1.1em;">Bạn chưa vượt qua bài tập phần: <br><b>${sectionTitle}</b></p>
                <p>Kết quả hiện tại: <b style="color:#f44336;">${correct}/${total}</b>. Bạn cần đúng 100%.</p>
                <button onclick="resetQuiz('${id}')" class="btn-check-section" style="background:#f44336; margin-top: 10px;">Làm lại ngay</button>
            </div>`;
    }

    container.innerHTML = htmlContent;
}
//hoàn thanh 100% thì qua bài tiếp
function goToNextSection(currentId) {
    const allContents = Array.from(document.querySelectorAll('.content'));
    const currentIndex = allContents.findIndex(c => c.id === currentId);

    if (currentIndex >= 0 && currentIndex < allContents.length - 1) {
        const nextContent = allContents[currentIndex + 1];

        // tìm link sidebar tương ứng với content
        const nextMenuLink = document.querySelector(
            `.sidebar_menu a[onclick*="'${nextContent.id}'"]`
        );

        //  vừa mở bài mới, vừa active sidebar
        showContent(nextContent.id, nextMenuLink);
    } else {
        alert('Bạn đã hoàn thành tất cả các bài học!');
    }
}


function handleAnswer(id, qIndex, value) {
    let savedData = JSON.parse(localStorage.getItem('html_progress')) || {};
    if (!savedData[id]) savedData[id] = {};
    savedData[id][qIndex] = value;
    localStorage.setItem('html_progress', JSON.stringify(savedData));
    updateProgressUI(id);
    renderQuiz(id);
}

function nextQuestion(id) {
    const total = quizData[id].length;
    if (currentQuestionIndex[id] < total - 1) {
        currentQuestionIndex[id]++;
        localStorage.setItem('quiz_step', JSON.stringify(currentQuestionIndex));
        renderQuiz(id);
    } else {
        renderQuiz(id); // Gọi lại để vào màn hình hoàn thành
    }
}

function resetQuiz(id) {
        let savedData = JSON.parse(localStorage.getItem('html_progress')) || {};
        delete savedData[id];
        localStorage.setItem('html_progress', JSON.stringify(savedData));

        currentQuestionIndex[id] = 0;
        localStorage.setItem('quiz_step', JSON.stringify(currentQuestionIndex));

        updateProgressUI(id);
        renderQuiz(id);
}

function updateProgressUI(id) {
    if (!quizData[id]) return;
    
    const questions = quizData[id];
    const savedData = JSON.parse(localStorage.getItem('html_progress')) || {};
    const userAnswers = savedData[id] || {};
    
    // Đếm số câu làm ĐÚNG
    let correctCount = 0;
    questions.forEach((item, index) => {
        if (userAnswers[index] !== undefined && parseInt(userAnswers[index]) === item.c) {
            correctCount++;
        }
    });

    const percent = Math.round((correctCount / questions.length) * 100);
    
    // Cập nhật con số % to ngang chữ ở Menu
    const menuProgText = document.getElementById(`menu-prog-${id}`);
    if (menuProgText) {
        menuProgText.innerText = '('+percent + '%)';
        menuProgText.style.fontSize = "15px"; // To ngang chữ menu
        menuProgText.style.fontWeight = "600";
        menuProgText.style.color = (percent === 100) ? "#03e47bff" : "#fc3535ff";
    }
// Cập nhật Thanh Bar trong nội dung bài học
    const fill = document.getElementById(`fill-${id}`);
    const text = document.getElementById(`percent-${id}`);
    if (fill) fill.style.width = percent + "%";
    if (text) text.innerText = percent;
}
window.onload = () => {
    // Tính % đúng cho tất cả các mục menu ngay khi load trang
    if (typeof quizData !== 'undefined') {
        Object.keys(quizData).forEach(id => {
            updateProgressUI(id);
        });
    }

    // Tự động vẽ Quiz cho mục đang hiển thị mặc định (thường là Home)
    const activeContent = document.querySelector('.content.active');
    if (activeContent) {
        renderQuiz(activeContent.id);
    }
};
function escapeHTML(str) {
    return str
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;");
}


//điểm danh    
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


 