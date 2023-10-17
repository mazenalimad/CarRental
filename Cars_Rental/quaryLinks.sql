CREATE DATABASE IF NOT EXISTS carrental;

USE carrental;

CREATE TABLE IF NOT EXISTS employees (
    id INT NOT NULL UNIQUE AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    ssn INT NOT NULL,
    hire_date DATE NOT NULL,
    birthday DATE NOT NULL,
    salary INT NOT NULL,
    phone INT NOT NULL,
    PRIMARY KEY(id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

ALTER TABLE employees AUTO_INCREMENT = 1;

CREATE TABLE IF NOT EXISTS users (
    id INT NOT NULL UNIQUE AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL,
    hash VARCHAR(255) NOT NULL,
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
	registation_paper INT NOT NULL,
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
    state VARCHAR(15) NOT NULL,
    renter_id INT,
    lessor_id INT,
    PRIMARY KEY(id),
    FOREIGN KEY(renter_id) REFERENCES renter(id),
    FOREIGN KEY(lessor_id) REFERENCES lessor(id)
)ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

ALTER TABLE car AUTO_INCREMENT = 1;


INSERT IGNORE INTO employees (id, name, ssn, hire_date, birthday, salary, phone) VALUES (1, 'Admin', 2048, NOW(), '2000-01-01', 20000, 777777777);

INSERT IGNORE INTO  users (id, username, hash, type, employees_id) VALUES (1, 'Admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'Admin', 1);

INSERT IGNORE INTO employees (id, name, ssn, hire_date, birthday, salary, phone) VALUES (2, 'employee', 4000, NOW(), '2008-01-01', 5000, 777777777);

INSERT IGNORE INTO  users (id, username, hash, type, employees_id) VALUES (2, 'employee', '2fdc0177057d3a5c6c2c0821e01f4fa8d90f9a3bb7afd82b0db526af98d68de8', 'Employees', 2);
