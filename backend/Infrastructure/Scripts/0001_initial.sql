-- Create the sequence for ProjectID
CREATE SEQUENCE project_id_seq
    START WITH 100000
    INCREMENT BY 1
    NO CYCLE;

-- Create construction_projects Table
CREATE TABLE construction_projects (
    project_id INT PRIMARY KEY DEFAULT nextval('project_id_seq'),
    project_name VARCHAR(200) NOT NULL,
    project_location VARCHAR(500) NOT NULL,
    stage VARCHAR(50) NOT NULL CHECK (stage IN ('Concept', 'Design & Documentation', 'Pre-Construction', 'Construction')),
    category VARCHAR(100) NOT NULL,
    other_category VARCHAR(100),
    start_date DATE NOT NULL,
    project_details TEXT,
    creator_id UUID NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL
);

-- Create users Table
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    email VARCHAR(200) UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL
);

-- Add Foreign Key Constraint
ALTER TABLE construction_projects
ADD CONSTRAINT fk_project_creator FOREIGN KEY (creator_id) REFERENCES users(id);
