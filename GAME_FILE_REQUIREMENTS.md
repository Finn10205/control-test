# Game File Review Requirements - Ngọc Rồng Online Multi-Box Bot

## 📋 Yêu Cầu Chung Khi Review Game File

Tất cả file game cấu hình phải đáp ứng các tiêu chí sau:

### 1. **Format & Cấu Trúc**

#### ✅ Bắt Buộc

- [ ] File phải ở định dạng **JSON** (hoặc YAML, XML nếu được phê duyệt)
- [ ] Phải có schema validation rõ ràng
- [ ] Không chứa comment không cần thiết
- [ ] Thụt lề đúng (2 hoặc 4 spaces)
- [ ] Không có trailing commas hoặc syntax errors

### 2. **Cấu Hình Skill (Skills Configuration)**

#### ✅ Bắt Buộc

- [ ] Mỗi skill phải có `id` unique (không trùng lặp)
- [ ] `name` phải rõ ràng, tiếng Việt hoặc tiếng Anh thống nhất
- [ ] `hotkey` phải là phím hợp lệ (A-Z, 0-9, F1-F12, Ctrl+X)
- [ ] `cooldown` phải >= 0 (tính bằng milliseconds)
- [ ] `delay` phải là số nguyên dương (delay giữa các click)

### 3. **Cấu Hình Hotkey (Hotkey Configuration)**

#### ✅ Bắt Buộc

- [ ] Hotkey phải follow chuẩn Windows
- [ ] Không được trùng lặp
- [ ] Phải định rõ loại: `single` (phím đơn) hoặc `combination` (phím tổ hợp)
- [ ] Mỗi hotkey phải map tới một action rõ ràng

### 4. **Cấu Hình Window & Sync (Window Manager)**

#### ✅ Bắt Buộc

- [ ] `windowCount` phải > 0
- [ ] `syncEnabled` phải là boolean
- [ ] `broadcastDelay` phải >= 0
- [ ] `windowNames` phải là array không trống

### 5. **Metadata & Thông Tin**

#### ✅ Bắt Buộc

- [ ] `version` phải theo format `X.Y.Z` (semantic versioning)
- [ ] `author` phải có tên hoặc nick name
- [ ] `createdDate` phải ở format `YYYY-MM-DD`
- [ ] `description` không được để trống

---

**Reviewer**: Kiểm tra tất cả tiêu chí trước khi approve!
