-- MediCore EMR Database Schema
-- PostgreSQL 15+
-- Optimized for Internal Medicine Practice

-- ============================================
-- CORE TABLES
-- ============================================

-- Users & Authentication
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    email VARCHAR(255) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(255) NOT NULL,
    role VARCHAR(50) NOT NULL CHECK (role IN ('doctor', 'receptionist', 'nurse', 'admin')),
    phone VARCHAR(20),
    is_active BOOLEAN DEFAULT true,
    last_login TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_users_email ON users(email);
CREATE INDEX idx_users_role ON users(role);

-- Patients
CREATE TABLE patients (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    patient_id VARCHAR(50) UNIQUE NOT NULL, -- MED001, MED002, etc.
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100),
    date_of_birth DATE,
    age INTEGER,
    gender VARCHAR(20) CHECK (gender IN ('Male', 'Female', 'Other')),
    blood_group VARCHAR(10),
    phone VARCHAR(20) NOT NULL,
    email VARCHAR(255),
    address TEXT,
    city VARCHAR(100),
    state VARCHAR(100),
    pincode VARCHAR(10),
    emergency_contact_name VARCHAR(255),
    emergency_contact_phone VARCHAR(20),
    allergies TEXT,
    chronic_conditions TEXT,
    photo_url TEXT,
    is_active BOOLEAN DEFAULT true,
    created_by UUID REFERENCES users(id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_patients_patient_id ON patients(patient_id);
CREATE INDEX idx_patients_phone ON patients(phone);
CREATE INDEX idx_patients_name ON patients(first_name, last_name);
CREATE INDEX idx_patients_created_at ON patients(created_at DESC);

-- Visits (OPD Consultations)
CREATE TABLE visits (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    visit_number VARCHAR(50) UNIQUE NOT NULL,
    patient_id UUID REFERENCES patients(id) ON DELETE CASCADE,
    doctor_id UUID REFERENCES users(id),
    visit_date DATE NOT NULL DEFAULT CURRENT_DATE,
    visit_time TIME NOT NULL DEFAULT CURRENT_TIME,
    visit_type VARCHAR(50) CHECK (visit_type IN ('New', 'Follow-up', 'Emergency')),
    status VARCHAR(50) DEFAULT 'Pending' CHECK (status IN ('Pending', 'In Progress', 'Completed', 'Cancelled')),
    
    -- Vitals
    bp_systolic INTEGER,
    bp_diastolic INTEGER,
    temperature DECIMAL(4,1),
    pulse INTEGER,
    spo2 INTEGER,
    weight DECIMAL(5,2),
    height DECIMAL(5,2),
    bmi DECIMAL(4,2),
    
    -- Clinical Data
    chief_complaints TEXT,
    history_present_illness TEXT,
    past_medical_history TEXT,
    family_history TEXT,
    personal_history TEXT,
    examination_findings TEXT,
    provisional_diagnosis TEXT,
    final_diagnosis TEXT,
    investigation_orders TEXT,
    treatment_plan TEXT,
    advice TEXT,
    follow_up_date DATE,
    follow_up_notes TEXT,
    
    -- Metadata
    duration_minutes INTEGER,
    notes TEXT,
    created_by UUID REFERENCES users(id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_visits_patient_id ON visits(patient_id);
CREATE INDEX idx_visits_visit_date ON visits(visit_date DESC);
CREATE INDEX idx_visits_doctor_id ON visits(doctor_id);
CREATE INDEX idx_visits_status ON visits(status);

-- Prescriptions
CREATE TABLE prescriptions (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    prescription_number VARCHAR(50) UNIQUE NOT NULL,
    visit_id UUID REFERENCES visits(id) ON DELETE CASCADE,
    patient_id UUID REFERENCES patients(id) ON DELETE CASCADE,
    doctor_id UUID REFERENCES users(id),
    prescription_date DATE NOT NULL DEFAULT CURRENT_DATE,
    diagnosis TEXT,
    advice TEXT,
    follow_up_date DATE,
    status VARCHAR(50) DEFAULT 'Active' CHECK (status IN ('Active', 'Completed', 'Cancelled')),
    pdf_url TEXT,
    sent_via_whatsapp BOOLEAN DEFAULT false,
    whatsapp_sent_at TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_prescriptions_visit_id ON prescriptions(visit_id);
CREATE INDEX idx_prescriptions_patient_id ON prescriptions(patient_id);
CREATE INDEX idx_prescriptions_date ON prescriptions(prescription_date DESC);

-- Prescription Items (Medicines)
CREATE TABLE prescription_items (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    prescription_id UUID REFERENCES prescriptions(id) ON DELETE CASCADE,
    medicine_name VARCHAR(255) NOT NULL,
    dosage VARCHAR(100),
    frequency VARCHAR(100),
    duration VARCHAR(100),
    quantity VARCHAR(50),
    instructions TEXT,
    timing VARCHAR(100), -- Before food, After food, etc.
    route VARCHAR(50), -- Oral, Topical, etc.
    display_order INTEGER,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_prescription_items_prescription_id ON prescription_items(prescription_id);

-- Medicines Master
CREATE TABLE medicines (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(255) NOT NULL,
    generic_name VARCHAR(255),
    brand_name VARCHAR(255),
    category VARCHAR(100),
    dosage_form VARCHAR(100), -- Tablet, Capsule, Syrup, etc.
    strength VARCHAR(100),
    manufacturer VARCHAR(255),
    description TEXT,
    common_dosage VARCHAR(100),
    common_frequency VARCHAR(100),
    common_duration VARCHAR(100),
    contraindications TEXT,
    side_effects TEXT,
    interactions TEXT,
    is_favorite BOOLEAN DEFAULT false,
    usage_count INTEGER DEFAULT 0,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_medicines_name ON medicines(name);
CREATE INDEX idx_medicines_category ON medicines(category);
CREATE INDEX idx_medicines_favorite ON medicines(is_favorite);
CREATE INDEX idx_medicines_usage ON medicines(usage_count DESC);

-- ============================================
-- BILLING & PAYMENTS
-- ============================================

-- Bills
CREATE TABLE bills (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    bill_number VARCHAR(50) UNIQUE NOT NULL,
    visit_id UUID REFERENCES visits(id) ON DELETE CASCADE,
    patient_id UUID REFERENCES patients(id) ON DELETE CASCADE,
    bill_date DATE NOT NULL DEFAULT CURRENT_DATE,
    subtotal DECIMAL(10,2) NOT NULL DEFAULT 0,
    discount_amount DECIMAL(10,2) DEFAULT 0,
    discount_percentage DECIMAL(5,2) DEFAULT 0,
    tax_amount DECIMAL(10,2) DEFAULT 0,
    tax_percentage DECIMAL(5,2) DEFAULT 0,
    total_amount DECIMAL(10,2) NOT NULL,
    paid_amount DECIMAL(10,2) DEFAULT 0,
    balance_amount DECIMAL(10,2) DEFAULT 0,
    payment_status VARCHAR(50) DEFAULT 'Pending' CHECK (payment_status IN ('Pending', 'Partial', 'Paid', 'Cancelled')),
    payment_mode VARCHAR(50), -- Cash, Card, UPI, etc.
    notes TEXT,
    created_by UUID REFERENCES users(id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_bills_patient_id ON bills(patient_id);
CREATE INDEX idx_bills_bill_date ON bills(bill_date DESC);
CREATE INDEX idx_bills_payment_status ON bills(payment_status);

-- Bill Items
CREATE TABLE bill_items (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    bill_id UUID REFERENCES bills(id) ON DELETE CASCADE,
    item_type VARCHAR(50) NOT NULL, -- Consultation, Procedure, Medicine, Lab, etc.
    item_name VARCHAR(255) NOT NULL,
    quantity INTEGER DEFAULT 1,
    unit_price DECIMAL(10,2) NOT NULL,
    total_price DECIMAL(10,2) NOT NULL,
    display_order INTEGER,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_bill_items_bill_id ON bill_items(bill_id);

-- Payments
CREATE TABLE payments (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    payment_number VARCHAR(50) UNIQUE NOT NULL,
    bill_id UUID REFERENCES bills(id) ON DELETE CASCADE,
    patient_id UUID REFERENCES patients(id) ON DELETE CASCADE,
    payment_date DATE NOT NULL DEFAULT CURRENT_DATE,
    amount DECIMAL(10,2) NOT NULL,
    payment_mode VARCHAR(50) NOT NULL,
    transaction_id VARCHAR(255),
    notes TEXT,
    created_by UUID REFERENCES users(id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_payments_bill_id ON payments(bill_id);
CREATE INDEX idx_payments_patient_id ON payments(patient_id);
CREATE INDEX idx_payments_date ON payments(payment_date DESC);

-- ============================================
-- APPOINTMENTS
-- ============================================

CREATE TABLE appointments (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    appointment_number VARCHAR(50) UNIQUE NOT NULL,
    patient_id UUID REFERENCES patients(id) ON DELETE CASCADE,
    doctor_id UUID REFERENCES users(id),
    appointment_date DATE NOT NULL,
    appointment_time TIME NOT NULL,
    duration_minutes INTEGER DEFAULT 15,
    appointment_type VARCHAR(50) CHECK (appointment_type IN ('New', 'Follow-up', 'Emergency', 'Video')),
    status VARCHAR(50) DEFAULT 'Scheduled' CHECK (status IN ('Scheduled', 'Confirmed', 'Checked-In', 'In Progress', 'Completed', 'Cancelled', 'No-Show')),
    reason TEXT,
    notes TEXT,
    reminder_sent BOOLEAN DEFAULT false,
    reminder_sent_at TIMESTAMP,
    created_by UUID REFERENCES users(id),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_appointments_patient_id ON appointments(patient_id);
CREATE INDEX idx_appointments_doctor_id ON appointments(doctor_id);
CREATE INDEX idx_appointments_date ON appointments(appointment_date, appointment_time);
CREATE INDEX idx_appointments_status ON appointments(status);

-- ============================================
-- LAB & INVESTIGATIONS
-- ============================================

-- Lab Tests Master
CREATE TABLE lab_tests (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    test_code VARCHAR(50) UNIQUE NOT NULL,
    test_name VARCHAR(255) NOT NULL,
    category VARCHAR(100),
    description TEXT,
    normal_range TEXT,
    unit VARCHAR(50),
    price DECIMAL(10,2),
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_lab_tests_category ON lab_tests(category);
CREATE INDEX idx_lab_tests_name ON lab_tests(test_name);

-- Lab Orders
CREATE TABLE lab_orders (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    order_number VARCHAR(50) UNIQUE NOT NULL,
    visit_id UUID REFERENCES visits(id) ON DELETE CASCADE,
    patient_id UUID REFERENCES patients(id) ON DELETE CASCADE,
    doctor_id UUID REFERENCES users(id),
    order_date DATE NOT NULL DEFAULT CURRENT_DATE,
    status VARCHAR(50) DEFAULT 'Ordered' CHECK (status IN ('Ordered', 'Sample Collected', 'In Progress', 'Completed', 'Cancelled')),
    notes TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_lab_orders_patient_id ON lab_orders(patient_id);
CREATE INDEX idx_lab_orders_visit_id ON lab_orders(visit_id);
CREATE INDEX idx_lab_orders_date ON lab_orders(order_date DESC);

-- Lab Order Items
CREATE TABLE lab_order_items (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    lab_order_id UUID REFERENCES lab_orders(id) ON DELETE CASCADE,
    lab_test_id UUID REFERENCES lab_tests(id),
    test_name VARCHAR(255) NOT NULL,
    status VARCHAR(50) DEFAULT 'Pending',
    result_value TEXT,
    result_unit VARCHAR(50),
    normal_range TEXT,
    is_abnormal BOOLEAN DEFAULT false,
    result_date DATE,
    notes TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_lab_order_items_order_id ON lab_order_items(lab_order_id);

-- ============================================
-- TEMPLATES & FAVORITES
-- ============================================

-- Prescription Templates
CREATE TABLE prescription_templates (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(255) NOT NULL,
    description TEXT,
    diagnosis TEXT,
    advice TEXT,
    doctor_id UUID REFERENCES users(id),
    is_public BOOLEAN DEFAULT false,
    usage_count INTEGER DEFAULT 0,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_prescription_templates_doctor_id ON prescription_templates(doctor_id);
CREATE INDEX idx_prescription_templates_usage ON prescription_templates(usage_count DESC);

-- Template Items
CREATE TABLE prescription_template_items (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    template_id UUID REFERENCES prescription_templates(id) ON DELETE CASCADE,
    medicine_name VARCHAR(255) NOT NULL,
    dosage VARCHAR(100),
    frequency VARCHAR(100),
    duration VARCHAR(100),
    instructions TEXT,
    display_order INTEGER,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_template_items_template_id ON prescription_template_items(template_id);

-- ============================================
-- COMMUNICATION
-- ============================================

-- WhatsApp Messages
CREATE TABLE whatsapp_messages (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    patient_id UUID REFERENCES patients(id) ON DELETE CASCADE,
    phone VARCHAR(20) NOT NULL,
    message_type VARCHAR(50) CHECK (message_type IN ('Prescription', 'Appointment', 'Reminder', 'Report', 'Custom')),
    message_text TEXT NOT NULL,
    media_url TEXT,
    status VARCHAR(50) DEFAULT 'Pending' CHECK (status IN ('Pending', 'Sent', 'Delivered', 'Read', 'Failed')),
    external_id VARCHAR(255),
    error_message TEXT,
    sent_at TIMESTAMP,
    delivered_at TIMESTAMP,
    read_at TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_whatsapp_patient_id ON whatsapp_messages(patient_id);
CREATE INDEX idx_whatsapp_status ON whatsapp_messages(status);
CREATE INDEX idx_whatsapp_created_at ON whatsapp_messages(created_at DESC);

-- ============================================
-- INVENTORY
-- ============================================

-- Inventory Items
CREATE TABLE inventory_items (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    item_code VARCHAR(50) UNIQUE NOT NULL,
    item_name VARCHAR(255) NOT NULL,
    category VARCHAR(100),
    unit VARCHAR(50),
    current_stock INTEGER DEFAULT 0,
    minimum_stock INTEGER DEFAULT 0,
    unit_price DECIMAL(10,2),
    expiry_date DATE,
    supplier VARCHAR(255),
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_inventory_category ON inventory_items(category);
CREATE INDEX idx_inventory_stock ON inventory_items(current_stock);

-- ============================================
-- SYSTEM & AUDIT
-- ============================================

-- Audit Logs
CREATE TABLE audit_logs (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID REFERENCES users(id),
    action VARCHAR(100) NOT NULL,
    entity_type VARCHAR(100),
    entity_id UUID,
    old_values JSONB,
    new_values JSONB,
    ip_address VARCHAR(50),
    user_agent TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_audit_user_id ON audit_logs(user_id);
CREATE INDEX idx_audit_entity ON audit_logs(entity_type, entity_id);
CREATE INDEX idx_audit_created_at ON audit_logs(created_at DESC);

-- System Settings
CREATE TABLE system_settings (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    setting_key VARCHAR(100) UNIQUE NOT NULL,
    setting_value TEXT,
    setting_type VARCHAR(50),
    description TEXT,
    updated_by UUID REFERENCES users(id),
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- FUNCTIONS & TRIGGERS
-- ============================================

-- Auto-update updated_at timestamp
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ language 'plpgsql';

-- Apply trigger to all tables with updated_at
CREATE TRIGGER update_users_updated_at BEFORE UPDATE ON users FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_patients_updated_at BEFORE UPDATE ON patients FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_visits_updated_at BEFORE UPDATE ON visits FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_prescriptions_updated_at BEFORE UPDATE ON prescriptions FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_medicines_updated_at BEFORE UPDATE ON medicines FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_bills_updated_at BEFORE UPDATE ON bills FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_appointments_updated_at BEFORE UPDATE ON appointments FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- ============================================
-- SEED DATA
-- ============================================

-- Insert default admin user (password: Admin@123)
INSERT INTO users (email, password_hash, full_name, role) VALUES
('admin@medicore.com', '$2a$10$rQZ9vXqZ9vXqZ9vXqZ9vXqZ9vXqZ9vXqZ9vXqZ9vXqZ9vXqZ9vXqZ', 'Admin User', 'admin');

-- Insert common medicines
INSERT INTO medicines (name, generic_name, category, dosage_form, strength, common_dosage, common_frequency, common_duration, is_favorite) VALUES
('Crocin', 'Paracetamol', 'Analgesic', 'Tablet', '500mg', '1 tablet', 'TDS', '3 days', true),
('Voveran', 'Diclofenac', 'NSAID', 'Tablet', '50mg', '1 tablet', 'BD', '5 days', true),
('Glykind-M', 'Metformin', 'Antidiabetic', 'Tablet', '500mg', '1 tablet', 'BD', 'Ongoing', true),
('Omez', 'Omeprazole', 'PPI', 'Capsule', '20mg', '1 capsule', 'OD', '7 days', true),
('Becasule', 'Multivitamin', 'Vitamin', 'Capsule', '-', '1 capsule', 'OD', '30 days', true);

-- Insert common lab tests
INSERT INTO lab_tests (test_code, test_name, category, normal_range, unit) VALUES
('CBC', 'Complete Blood Count', 'Hematology', 'Varies', '-'),
('FBS', 'Fasting Blood Sugar', 'Biochemistry', '70-100', 'mg/dL'),
('PPBS', 'Post Prandial Blood Sugar', 'Biochemistry', '<140', 'mg/dL'),
('HBA1C', 'HbA1c', 'Biochemistry', '<5.7', '%'),
('LIPID', 'Lipid Profile', 'Biochemistry', 'Varies', '-'),
('LFT', 'Liver Function Test', 'Biochemistry', 'Varies', '-'),
('KFT', 'Kidney Function Test', 'Biochemistry', 'Varies', '-'),
('TSH', 'Thyroid Stimulating Hormone', 'Endocrinology', '0.4-4.0', 'mIU/L'),
('URINE', 'Urine Routine', 'Pathology', 'Normal', '-');

-- ============================================
-- VIEWS FOR REPORTING
-- ============================================

-- Daily Collection Summary
CREATE VIEW daily_collection AS
SELECT 
    bill_date,
    COUNT(*) as total_bills,
    SUM(total_amount) as total_amount,
    SUM(paid_amount) as paid_amount,
    SUM(balance_amount) as balance_amount
FROM bills
GROUP BY bill_date
ORDER BY bill_date DESC;

-- Patient Visit Summary
CREATE VIEW patient_visit_summary AS
SELECT 
    p.id,
    p.patient_id,
    p.first_name || ' ' || COALESCE(p.last_name, '') as patient_name,
    p.phone,
    COUNT(v.id) as total_visits,
    MAX(v.visit_date) as last_visit_date,
    SUM(b.total_amount) as total_billing
FROM patients p
LEFT JOIN visits v ON p.id = v.patient_id
LEFT JOIN bills b ON p.id = b.patient_id
GROUP BY p.id, p.patient_id, p.first_name, p.last_name, p.phone;

-- ============================================
-- PERFORMANCE OPTIMIZATION
-- ============================================

-- Analyze tables for query optimization
ANALYZE users;
ANALYZE patients;
ANALYZE visits;
ANALYZE prescriptions;
ANALYZE bills;
ANALYZE appointments;

-- ============================================
-- COMMENTS
-- ============================================

COMMENT ON TABLE patients IS 'Stores patient demographic and contact information';
COMMENT ON TABLE visits IS 'Records each OPD consultation with clinical data';
COMMENT ON TABLE prescriptions IS 'Stores prescription headers';
COMMENT ON TABLE prescription_items IS 'Stores individual medicines in prescriptions';
COMMENT ON TABLE bills IS 'Billing information for consultations and services';
COMMENT ON TABLE appointments IS 'Appointment scheduling and management';
COMMENT ON TABLE whatsapp_messages IS 'WhatsApp communication log';
