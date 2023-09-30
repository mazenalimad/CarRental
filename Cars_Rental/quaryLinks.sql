CREATE DATABASE IF NOT EXISTS carrental;

CREATE TABLE IF NOT EXISTS employees (
    id INT NOT NULL UNIQUE AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    ssn INT NOT NULL,
    hire_date DATE NOT NULL,
    birthday DATE NOT NULL,
    salary INT NOT NULL
)ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

ALTER TABLE employees AUTO_INCREMENT = 1;

CREATE TABLE IF NOT EXISTS users (
    id INT NOT NULL UNIQUE AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL,
    hash VARCHAR(50) NOT NULL,
    type VARCHAR(10),
    employees_id INT,
    PRIMARY KEY(id),
    FOREIGN KEY(employees_id) REFERENCES employees(id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

ALTER TABLE users AUTO_INCREMENT = 1;

CREATE TABLE IF NOT EXISTS client (
    id INT NOT NULL UNIQUE AUTO_INCREMENT,
    SSN INT NOT NULL,
    name VARCHAR(50) NOT NULL,
    phone INT NOT NULL,
	PRIMARY KEY(id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

ALTER TABLE client AUTO_INCREMENT = 1;

CREATE TABLE IF NOT EXISTS renter (
	id INT NOT NULL UNIQUE AUTO_INCREMENT,
	payment_method VARCHAR(50),
	drive_licanese VARCHAR(50),
    received_date DATE,
    due_date DATE,
    client_id INT,
    users_id INT,
	PRIMARY KEY(id),
	FOREIGN KEY(client_id) REFERENCES client(id),
    FOREIGN KEY(users_id) REFERENCES users(id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

ALTER TABLE renter AUTO_INCREMENT = 1;

CREATE TABLE IF NOT EXISTS lessor (
	id INT NOT NULL UNIQUE AUTO_INCREMENT,
	commission DOUBLE NOT NULL,
	registation_paper VARCHAR(50) NOT NULL,
    client_id INT,
	PRIMARY KEY(id),
	FOREIGN KEY(client_id) REFERENCES client(id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

ALTER TABLE lessor AUTO_INCREMENT = 1;

CREATE TABLE IF NOT EXISTS car (
    id INT NOT NULL UNIQUE AUTO_INCREMENT,
    Brand VARCHAR(50) NOT NULL,
    model VARCHAR(50) NOT NULL,
    year INT NOT NULL,
    plateNumber VARCHAR(20) NOT NULL,
    price INT NOT NULL,
    state VARCHAR(10) NOT NULL,
    renter_id INT,
    lessor_id INT,
    PRIMARY KEY(id),
    FOREIGN KEY(renter_id) REFERENCES renter(id),
    FOREIGN KEY(lessor_id) REFERENCES lessor(id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

ALTER TABLE car AUTO_INCREMENT = 1;