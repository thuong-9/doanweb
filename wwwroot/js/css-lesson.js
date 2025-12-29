// File css-lesson.js
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

const quizData = {
    'Home': [
        { q: "CSS là viết tắt của cụm từ nào?", o: ["Cascading Style Sheets", "Creative Style Sheets", "Computer Style Sheets", "Colorful Style Sheets"], c: 0 },
        { q: "CSS được phát triển bởi tổ chức nào?", o: ["Google", "W3C", "Microsoft", "Mozilla"], c: 1 },
        { q: "Phiên bản CSS đầu tiên ra đời vào năm nào?", o: ["1994", "1995", "1996", "1997"], c: 2 },
        { q: "Mối quan hệ giữa HTML và CSS là gì?", o: ["CSS tạo cấu trúc, HTML tạo giao diện", "HTML tạo cấu trúc, CSS tạo giao diện", "Cả hai đều tạo logic xử lý", "Không có mối quan hệ nào"], c: 1 },
        { q: "Thuộc tính nào giúp trang web hiển thị tốt trên nhiều thiết bị (Responsive)?", o: ["Animation", "Flexbox & Grid", "Table layout", "Marquee"], c: 1 }
    ],
    'Syntax': [
        { q: "Đâu là cú pháp CSS đúng để thay đổi màu chữ của thẻ &lt;p&gt;?", o: ["p {color: red;}", "p: color=red;", "{p: color:red}", "p {font-color: red;}"], c: 0 },
        { q: "Trong câu lệnh 'h1 {font-size: 12px;}', phần 'font-size' được gọi là gì?", o: ["Selector", "Value", "Property", "Declaration"], c: 2 },
        { q: "Ký tự nào dùng để ngăn cách giữa Property và Value?", o: ["Dấu chấm (.)", "Dấu phẩy (,)", "Dấu chấm phẩy (;)", "Dấu hai chấm (:)"], c: 3 },
        { q: "Dấu nào dùng để kết thúc một dòng khai báo (Declaration)?", o: ["Dấu chấm phẩy (;)", "Dấu hai chấm (:)", "Dấu ngoặc đơn ()", "Dấu ngoặc nhọn {}"], c: 0 },
        { q: "Phần Selector trong CSS dùng để làm gì?", o: ["Để đặt tên cho biến", "Để chọn phần tử HTML muốn định dạng", "Để viết ghi chú", "Để khai báo màu sắc"], c: 1 }
    ],
    'Selectors': [
        { q: "Để chọn một phần tử có id là 'header', ta dùng ký tự nào?", o: [".header", "#header", "*header", "header"], c: 1 },
        { q: "Để chọn tất cả các thẻ &lt;a&gt; nằm bên trong thẻ &lt;div&gt;, ta dùng bộ chọn nào?", o: ["div.a", "div + a", "div a", "div &gt; a (chỉ con trực tiếp)"], c: 2 },
        { q: "Bộ chọn lớp giả (Pseudo-class) nào dùng khi di chuột qua phần tử?", o: [":active", ":focus", ":visited", ":hover"], c: 3 },
        { q: "Ký tự '*' trong CSS Selector có ý nghĩa gì?", o: ["Chọn tất cả các thẻ &lt;div&gt;", "Chọn tất cả các phần tử trên trang", "Chọn phần tử đầu tiên", "Không có ý nghĩa"], c: 1 },
        { q: "Để chọn các phần tử có class là 'note', ta dùng cú pháp nào?", o: ["#note", "*note", ".note", "note"], c: 2 }
    ],
    'Cachthuchien': [
        { q: "Cách nào sau đây không phải là cách chèn CSS vào HTML?",o: ["Inline CSS", "Internal CSS", "External CSS", "Embedded CSS"],c: 3 },
        { q: "Để liên kết một file CSS bên ngoài vào HTML, ta dùng thẻ nào?",o: ["&lt;script&gt;", "&lt;link&gt;", "&lt;style&gt;", "&lt;css&gt;"],c: 1 },
        { q: "Trong thẻ &lt;link&gt;, thuộc tính nào chỉ định đường dẫn đến file CSS?", o: ["type", "href", "rel", "src"], c: 1 },
        { q: "Để viết CSS nội tuyến (inline) cho một thẻ HTML, ta sử dụng thuộc tính nào?", o: ["class", "id", "style", "css"], c: 2 },
        { q: "Thẻ &lt;style&gt; thường được đặt ở đâu trong tài liệu HTML?",o: ["Trong thẻ &lt;body&gt;", "Trong thẻ &lt;footer&gt;", "Trong thẻ &lt;head&gt;", "Bên ngoài thẻ &lt;html&gt;"], c: 2 }
    ],
    'Comment': [
        { q: "Cú pháp ghi chú (comment) đúng trong CSS là gì?", o: ["// ghi chú", "/* ghi chú", "/* ghi chú */", "# ghi chú"], c: 3 },
        { q: "Ghi chú trong CSS có hiển thị trên trình duyệt không?", o: ["Có hiển thị", "Không hiển thị", "Chỉ hiển thị khi lỗi", "Hiển thị dạng popup"], c: 1 },
        { q: "Ta có thể dùng ghi chú để làm gì?", o: ["Vô hiệu hóa tạm thời code", "Giải thích đoạn mã", "Phân chia bố cục file", "Tất cả các ý trên"], c: 3 },
        { q: "Ghi chú CSS có thể kéo dài trên nhiều dòng không?", o: ["Có", "Không", "Chỉ tối đa 2 dòng", "Chỉ dùng được trong thẻ &lt;p&gt;"], c: 0 },
        { q: "Để kết thúc một đoạn ghi chú CSS, ta dùng ký tự nào?", o: ["*/", "//", "--&gt;", "*/;"], c: 0 }
    ],
    'Color': [
        { q: "Giá trị màu RGB(255, 255, 255) đại diện cho màu nào?", o: ["Màu Đen", "Màu Trắng", "Màu Đỏ", "Màu Xanh"], c: 1 },
        { q: "Mã màu HEX bắt đầu bằng ký tự nào?", o: [".", "$", "&", "#"], c: 3 },
        { q: "Trong RGBA, chữ 'A' (Alpha) dùng để điều chỉnh cái gì?", o: ["Độ sáng", "Độ bão hòa", "Độ trong suốt", "Tên màu"], c: 2 },
        { q: "Hệ màu HSL bao gồm những thành phần nào?", o: ["Hue, Saturation, Lightness", "High, Soft, Low", "Hard, Solid, Light", "Hue, Sharp, Long"], c: 0 },
        { q: "Giá trị Alpha trong RGBA nằm trong khoảng nào?", o: ["0 - 100", "0 - 255", "0.0 - 1.0", "1 - 10"], c: 2 }
    ],
    'Nen': [
        { q: "Thuộc tính nào dùng để chèn hình ảnh làm nền?", o: ["background-color", "background-image", "img-background", "content-image"], c: 1 },
        { q: "Để ảnh nền không bị lặp lại, ta dùng giá trị nào?", o: ["repeat: no", "background-repeat: none", "background-repeat: no-repeat", "repeat: zero"], c: 2 },
        { q: "Thuộc tính nào giúp ảnh nền cố định khi cuộn trang?", o: ["background-scroll: fixed", "background-attachment: fixed", "background-position: fixed", "background-fixed: true"], c: 1 },
        { q: "Thứ tự rút gọn (shorthand) của background nào là đúng?", o: ["color, image, repeat, attachment, position", "image, color, position, repeat", "repeat, image, color, attachment", "Không quan trọng thứ tự"], c: 0 },
        { q: "Giá trị nào của background-size giúp ảnh bao phủ toàn bộ vùng chứa?", o: ["contain", "cover", "fill", "100%"], c: 1 }
    ],
    'Border': [
        { q: "Thuộc tính nào xác định kiểu dáng đường viền (nét đứt, nét liền...)?", o: ["border-width", "border-color", "border-style", "border-type"], c: 2 },
        { q: "Giá trị 'dashed' của border-style tạo ra đường viền dạng gì?", o: ["Nét liền", "Nét đứt quãng", "Dấu chấm", "Đường đôi"], c: 1 },
        { q: "Làm sao để đặt viền chỉ cho cạnh dưới của phần tử?", o: ["border-bottom", "border-down", "border-under", "margin-bottom"], c: 0 },
        { q: "Để làm bo tròn các góc của đường viền, ta dùng thuộc tính nào?", o: ["border-style", "border-circle", "border-radius", "corner-radius"], c: 2 },
        { q: "Cú pháp rút gọn 'border: 1px solid red;' bao gồm những gì?", o: ["width, style, color", "style, color, radius", "width, height, color", "color, style, position"], c: 0 }
    ],
    'Margins': [
        { q: "Margin là khoảng cách ở đâu?", o: ["Bên trong đường viền", "Bên ngoài đường viền", "Giữa chữ và ảnh", "Độ dày đường viền"], c: 1 },
        { q: "Cú pháp 'margin: 10px 20px;' có nghĩa là gì?", o: ["Trên-Dưới 10px, Trái-Phải 20px", "Trái-Phải 10px, Trên-Dưới 20px", "Cả 4 cạnh 10px", "Cả 4 cạnh 20px"], c: 0 },
        { q: "Margin có thể nhận giá trị âm không?", o: ["Có", "Không", "Chỉ margin-top mới có", "Chỉ margin-left mới có"], c: 0 },
        { q: "Khi hai lề dọc gặp nhau, hiện tượng gì xảy ra?", o: ["Lề cộng dồn", "Lề bị mất", "Sụp đổ lề (chọn giá trị lớn nhất)", "Lề đẩy nhau ra"], c: 2 },
        { q: "Giá trị 'auto' trong margin thường dùng để làm gì?", o: ["Tăng kích thước thẻ", "Căn giữa phần tử theo chiều ngang", "Căn giữa phần tử theo chiều dọc", "Làm ẩn phần tử"], c: 1 }
    ],
    'Dem': [
        { q: "Padding là khoảng cách ở đâu?", o: ["Bên ngoài đường viền", "Bên trong đường viền và nội dung", "Khoảng cách giữa các thẻ &lt;p&gt;", "Độ dày của font chữ"], c: 1 },
        { q: "Padding có được phép nhận giá trị âm không?", o: ["Có", "Không", "Tùy trình duyệt", "Chỉ padding-bottom được phép"], c: 1 },
        { q: "Cú pháp 'padding: 10px 5px 15px 20px;' thì padding-left là bao nhiêu?", o: ["10px", "5px", "15px", "20px"], c: 3 },
        { q: "Padding ảnh hưởng như thế nào đến kích thước thực tế của phần tử (mặc định)?", o: ["Không ảnh hưởng", "Làm giảm kích thước", "Làm tăng kích thước tổng thể", "Làm phần tử biến mất"], c: 2 },
        { q: "Để tính toán kích thước bao gồm cả padding, ta dùng thuộc tính nào?", o: ["display: block", "box-sizing: border-box", "overflow: hidden", "margin: 0"], c: 1 }
    ],
    'Thanhdieuhuong': [
        { q: "Thanh điều hướng thường được xây dựng bằng các phần tử HTML nào?", o: ["&lt;div&gt; và &lt;span&gt;", "&lt;ul&gt; và &lt;li&gt;", "&lt;table&gt; và &lt;tr&gt;", "&lt;nav&gt; và &lt;a&gt;"], c: 1 },
        { q: "Để xóa dấu đầu dòng khỏi danh sách trong thanh điều hướng, ta dùng thuộc tính nào?", o: ["list-style-type: none;", "text-decoration: none;", "display: none;", "border: none;"], c: 0 },
        { q: "Thuộc tính nào giúp căn giữa các liên kết trong thanh điều hướng ngang?", o: ["text-align: center;", "justify-content: center;", "align-items: center;", "margin: auto;"], c: 1 },
        { q: "Để làm nổi bật liên kết hiện tại trong thanh điều hướng, ta thường sử dụng gì?", o: ["Pseudo-element", "Pseudo-class :active", "Class 'active'", "ID selector"], c: 2 },
        { q: "Thuộc tính nào giúp thay đổi màu nền của liên kết khi di chuột qua nó?", o: [":hover", ":focus", ":visited", ":active"], c: 0 }
    ]
};

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
<<<<<<< HEAD
    const sectionTitle = document.querySelector(`#${id} .main-title`)?.innerText || "bài học";

    let htmlContent = "";

    if (scorePercent == 100) {
        // TRƯỜNG HỢP BẰNG 100 - CHÚC MỪNG
        htmlContent = `
            <div class="khung" style="text-align:center; border: 2px solid #04AA6D; padding: 30px; border-radius: 15px; background: #f0fff4; animation: fadeIn 0.5s;">
                <h3 style="color:#04AA6D;">🎉 Tuyệt vời!</h3>
                <p style="font-size: 1.1em;">Bạn đã xuất sắc vượt qua bài tập phần: <b style="color:#2c3e50; font-size: 1.2em;">${sectionTitle}</b></p>
=======
const sectionTitle = document.querySelector(`#${id} .main-title`)?.innerText || "bài học";

    let htmlContent = "";

    if (scorePercent ===100) {
        // TRƯỜNG HỢP đạt 100% - CHÚC MỪNG
        htmlContent = `
            <div class="khung" style="text-align:center; border: 2px solid #04AA6D; padding: 30px; border-radius: 15px; background: #f0fff4; animation: fadeIn 0.5s;">
                <h3 style="color:#04AA6D;">🎉 Tuyệt vời!</h3>
                <p style="font-size: 1.1em;">Bạn đã xuất sắc vượt qua bài tập phần: <br><b style="color:#2c3e50; font-size: 1.2em;">${sectionTitle}</b></p>
>>>>>>> 7705cc3bc64ca8111e23686d4cb4e5ae50a29d64
                <div style="margin: 20px 0;">
                    <span style="font-size: 2em; font-weight: bold; color: #04AA6D;">${correct}/${total}</span>
                    <p>Câu trả lời chính xác (${scorePercent}%)</p>
                </div>
<<<<<<< HEAD
                <button onclick="resetQuiz('${id}')" class="btn-check-section" style="background:#607d8b; margin-top: 10px;">Làm lại bài tập</button>
            </div>`;
    } else {
        // TRƯỜNG HỢP DƯỚI 50% - YÊU CẦU LÀM LẠI
        htmlContent = `
            <div class="khung" style="text-align:center; border: 2px solid #f44336; padding: 30px; border-radius: 15px; background: #fff5f5; animation: shake 0.5s;">
                <h3 style="color:#f44336;">⚠️ Cố gắng lên!</h3>
                <p style="font-size: 1.1em;">Bạn chưa vượt qua bài tập phần: <b>${sectionTitle}</b></p>
=======
            <button onclick="goToNextSection('${id}')" class="btn-check-section" style="background:#2196F3; margin-top: 10px;">Qua bài tiếp theo</button>            </div>`;
    } else {
        // TRƯỜNG HỢP DƯỚI 100% - YÊU CẦU LÀM LẠI
        htmlContent = `
            <div class="khung" style="text-align:center; border: 2px solid #f44336; padding: 30px; border-radius: 15px; background: #fff5f5; animation: shake 0.5s;">
                <h3 style="color:#f44336;">⚠️ Cố gắng lên!</h3>
                <p style="font-size: 1.1em;">Bạn chưa vượt qua bài tập phần: <br><b>${sectionTitle}</b></p>
>>>>>>> 7705cc3bc64ca8111e23686d4cb4e5ae50a29d64
                <p>Kết quả hiện tại: <b style="color:#f44336;">${correct}/${total}</b>. Bạn cần đúng 100%.</p>
                <button onclick="resetQuiz('${id}')" class="btn-check-section" style="background:#f44336; margin-top: 10px;">Làm lại ngay</button>
            </div>`;
    }

    container.innerHTML = htmlContent;
<<<<<<< HEAD
=======
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
>>>>>>> 7705cc3bc64ca8111e23686d4cb4e5ae50a29d64
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
    
    // 1. Tính số câu làm ĐÚNG
    let correctCount = 0;
    questions.forEach((item, index) => {
        if (userAnswers[index] !== undefined && parseInt(userAnswers[index]) === item.c) {
            correctCount++;
        }
    });

    const percent = Math.round((correctCount / questions.length) * 100);
    const totalQuestions = questions.length;
    const answeredCount = Object.keys(userAnswers).length;

    // 2. Cập nhật số % ở Menu
    const menuProgText = document.getElementById(`menu-prog-${id}`);
    if (menuProgText) {
<<<<<<< HEAD
        if (answeredCount === totalQuestions && percent >= 0) {
            menuProgText.innerText = percent + "%";
            menuProgText.style.display = "inline"; // Hiện số %

            // Đổi màu: 100% màu xanh, dưới 100% màu đỏ
            if (percent === 100) {
                menuProgText.classList.remove('percent-incomplete');
                menuProgText.classList.add('percent-perfect');
            } else {
                menuProgText.classList.remove('percent-perfect');
                menuProgText.classList.add('percent-incomplete');
            }
    }

    // 3. Cập nhật Thanh Bar trong nội dung (nếu có)
=======
        menuProgText.innerText = '('+percent + '%)';
        menuProgText.style.fontSize = "15px"; // To ngang chữ menu
        menuProgText.style.fontWeight = "600";
        menuProgText.style.color = (percent === 100) ? "#03e47bff" : "#fc3535ff";
    }
// Cập nhật Thanh Bar trong nội dung bài học
>>>>>>> 7705cc3bc64ca8111e23686d4cb4e5ae50a29d64
    const fill = document.getElementById(`fill-${id}`);
    if (fill) fill.style.width = percent + "%";
}
}
<<<<<<< HEAD

function markAsFinished(id) {
    // Cập nhật giao diện để hiện % màu đỏ/xanh tương ứng
    updateProgressUI(id);
    
    alert("Chúc mừng bạn đã hoàn thành phần: " + id);

    // Chuyển sang bài học tiếp theo (Logic tự động)
    const menuLinks = Array.from(document.querySelectorAll('.list li a'));
    const currentIndex = menuLinks.findIndex(link => link.getAttribute('onclick').includes(`'${id}'`));
    
    if (currentIndex !== -1 && currentIndex < menuLinks.length - 1) {
        const nextLink = menuLinks[currentIndex + 1];
        nextLink.click(); 
    }
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



=======
>>>>>>> 7705cc3bc64ca8111e23686d4cb4e5ae50a29d64
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
