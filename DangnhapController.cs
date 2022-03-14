public class DangNhapController : Controller{
    public ActionResult DangNhap(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                KhachHang kh = db.KhachHang.SingleOrDefault(p => p.UserPhone  == model.UserPhone  || p.UserEmail  == model.UserEmail &&
               passwordHasher.VerifyHashedPassword(p.UserPassword, model.UserPassword) == PasswordVerificationResult.Success);
                if (kh == null)
                {
                    ModelState.AddModelError("Loi", "Thông tin tài khoản hoặc mật khẩu không hợp lệ.");
                    return View("Index");
                }
                else
                {
                    HttpContext.Session.Set("TaiKhoan", kh);
                    return RedirectToAction("Index", "Home");
                }

            }
            return View("Index");
        }
}