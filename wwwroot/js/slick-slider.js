$(document).ready(function(){
  $('.center').slick({
    centerMode: true,
    centerPadding: '0px',
    slidesToShow: 3,
    autoplay: true,
    autoplaySpeed: 3000,
    arrows: true,
    responsive: [
      {
        breakpoint: 768,
        settings: {
          arrows: false,
          centerMode: true,
          centerPadding: '40px',
          slidesToShow: 2
        }
      },
      {
        breakpoint: 480,
        settings: {
          arrows: false,
          centerMode: true,
          centerPadding: '20px',
          slidesToShow: 1
        }
      }
    ]
  });
});

$(document).ready(function () {
  // 1️⃣ Khi nhấn nút "Thử ngay"
  $(".btn-learn").on("click", function (e) {
    e.preventDefault(); // ✅ chặn cuộn lên đầu trang

    const editorSection = $("#live-editor");
    const practiceSection = $(".practice");

    // Ẩn phần practice (các card)
    practiceSection.slideUp(400);

    // Hiển thị phần editor nếu đang ẩn
    if (editorSection.is(":hidden")) {
      editorSection.slideDown(600);
    }

    // Cuộn mượt xuống phần editor
    $("html, body").animate(
      {
        scrollTop: editorSection.offset().top - 60,
      },
      600
    );

    // Lấy loại editor từ nút bấm
    const type = $(this).data("editor");

    // Thiết lập nội dung mẫu khác nhau
    if (type === "html") {
      $("#html-code").val(
        `<h1>Chào mừng bạn đến với HTML!</h1>\n<p>Đây là vùng thực hành HTML đầu tiên.</p>`
      );
      $("#css-code").val(
        `body { text-align: center; color: #0078ff; font-family: Arial; }`
      );
    } else if (type === "css") {
      $("#html-code").val(`<div class="box">EasyCode CSS</div>`);
      $("#css-code").val(
        `.box {
  width:200px;
  height:100px;
  background:#0078ff;
  color:white;
  display:flex;
  align-items:center;
  justify-content:center;
  border-radius:8px;
  margin:40px auto;
  font-weight:600;
}`
      );
    } else if (type === "layout") {
      $("#html-code").val(
        `<div class="container"><div>1</div><div>2</div><div>3</div></div>`
      );
      $("#css-code").val(
        `.container {
  display:flex;
  gap:10px;
  justify-content:center;
}
.container div {
  background:#0078ff;
  color:white;
  padding:20px;
  border-radius:8px;
  width:60px;
  text-align:center;
}`
      );
    }

    // Tự chạy lần đầu
    $("#run-btn").trigger("click");
  });

  // 2️⃣ Khi nhấn "Chạy thử"
  $("#run-btn").on("click", function (e) {
    e.preventDefault(); // tránh reload
    const htmlCode = $("#html-code").val();
    const cssCode = `<style>${$("#css-code").val()}</style>`;
    const output = cssCode + htmlCode;

    const iframe = document.getElementById("preview");
    const iframeDoc = iframe.contentDocument || iframe.contentWindow.document;
    iframeDoc.open();
    iframeDoc.write(output);
    iframeDoc.close();
  });

  // 3️⃣ (Tùy chọn) Nút quay lại danh sách thử ngay
  // Nếu bạn thêm <button id="back-btn">Quay lại</button> vào live-editor
  $("#back-btn").on("click", function () {
    $("#live-editor").slideUp(500);
    $(".practice").slideDown(500);

    $("html, body").animate(
      {
        scrollTop: $(".practice").offset().top - 60,
      },
      600
    );
  });
});
