document.addEventListener("DOMContentLoaded", function () {
  const authBox = document.getElementById('auth-box');
  const switchSignUp = document.getElementById('switchSignUp');
  const switchSignIn = document.getElementById('switchSignIn');

  if (switchSignUp) {
    switchSignUp.addEventListener('click', () => authBox.classList.add('active'));
  }

  if (switchSignIn) {
    switchSignIn.addEventListener('click', () => authBox.classList.remove('active'));
  }

  // 🟢 Kiểm tra tham số mode trong URL
  const params = new URLSearchParams(window.location.search);
  if (params.get("mode") === "signup") {
    authBox.classList.add("active");
  } else {
    authBox.classList.remove("active");
  }
});
