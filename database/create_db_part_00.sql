DROP VIEW IF EXISTS view_user_account;
DROP VIEW IF EXISTS view_user_info;

DROP TABLE IF EXISTS list_user;
DROP TABLE IF EXISTS list_details;
DROP TABLE IF EXISTS list_password;
DROP TABLE IF EXISTS list_country;
DROP TABLE IF EXISTS list_gender;
DROP TABLE IF EXISTS list_account_type;

-- ------------------------------------------------------------------------

CREATE TABLE list_account_type (
	num serial NOT NULL,
	name_account_type varchar NOT NULL DEFAULT 'NA',
	CONSTRAINT list_account_type_pk PRIMARY KEY (num)
);

CREATE TABLE list_gender (
	num serial NOT NULL,
	name_gender varchar NOT NULL DEFAULT 'NA',
	CONSTRAINT list_gender_pk PRIMARY KEY (num)
);

CREATE TABLE list_country (
	num serial NOT NULL,
	name_country varchar NOT NULL DEFAULT 'NA',
	CONSTRAINT list_country_pk PRIMARY KEY (num)
);

CREATE TABLE list_password (
	num serial NOT NULL,
	user_password varchar NOT NULL DEFAULT 'NA',
	need_reset bool NOT NULL DEFAULT true,
    last_update timestamp NOT NULL,
	CONSTRAINT list_password_pk PRIMARY KEY (num)
);

CREATE TABLE list_details (
	num serial NOT NULL,
	user_name varchar NOT NULL DEFAULT 'NA',
	code_gender int NOT NULL DEFAULT 0,
	year_of_birth varchar NOT NULL DEFAULT 'NA',
	code_country int NOT NULL DEFAULT 0,
	user_email varchar NOT NULL DEFAULT 'NA',
    last_update timestamp NOT NULL,
    CONSTRAINT list_details_pk PRIMARY KEY (num),
	CONSTRAINT list_details_fk FOREIGN KEY (code_gender) REFERENCES list_gender(num) ON UPDATE CASCADE,
	CONSTRAINT list_details_fk_1 FOREIGN KEY (code_country) REFERENCES list_country(num) ON UPDATE CASCADE
);

CREATE TABLE list_user (
	username varchar NOT NULL DEFAULT 'NA',
	is_active bool NOT NULL DEFAULT true,
	code_account_type int NOT NULL DEFAULT 0,
	code_password int NOT NULL DEFAULT 0,
    code_details int NOT NULL DEFAULT 0,
	CONSTRAINT list_user_pk PRIMARY KEY (username),
	CONSTRAINT list_user_fk FOREIGN KEY (code_account_type) REFERENCES list_account_type(num) ON UPDATE CASCADE,
	CONSTRAINT list_user_fk_1 FOREIGN KEY (code_password) REFERENCES list_password(num) ON UPDATE CASCADE,
    CONSTRAINT list_user_fk_2 FOREIGN KEY (code_details) REFERENCES list_details(num) ON UPDATE CASCADE
);

-- ------------------------------------------------------------------------

CREATE VIEW view_user_account AS
SELECT
    list_user.username AS username,
    list_user.is_active AS account_active,
    list_account_type.name_account_type AS account_type,
    list_password.user_password AS account_password,
    list_password.need_reset AS password_require_reset,
    list_password.last_update AS password_update_on,
    list_user.code_details AS code_user_details
FROM
    list_user
LEFT OUTER JOIN list_account_type ON list_user.code_account_type = list_account_type.num
LEFT OUTER JOIN list_password ON list_user.code_password = list_password.num;

CREATE VIEW view_user_info AS
SELECT
    list_user.username AS username,
    list_details.user_name AS user_name,
    list_gender.name_gender AS user_gender,
    list_details.year_of_birth AS user_year_birth,
    list_country.name_country AS user_country,
    list_details.user_email AS user_email,
    list_details.last_update AS info_update_on
FROM
    list_user
LEFT OUTER JOIN list_details ON list_user.code_details = list_details.num
LEFT OUTER JOIN list_gender ON list_details.code_gender = list_gender.num
LEFT OUTER JOIN list_country ON list_details.code_country = list_country.num;
