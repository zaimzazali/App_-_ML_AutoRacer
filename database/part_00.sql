DROP VIEW IF EXISTS "schema-ML_AutoRacer".view_user_account;
DROP VIEW IF EXISTS "schema-ML_AutoRacer".view_user_info;

DROP TABLE IF EXISTS "schema-ML_AutoRacer".user;
DROP TABLE IF EXISTS "schema-ML_AutoRacer".list_details;
DROP TABLE IF EXISTS "schema-ML_AutoRacer".list_password;
DROP TABLE IF EXISTS "schema-ML_AutoRacer".list_country;
DROP TABLE IF EXISTS "schema-ML_AutoRacer".list_gender;
DROP TABLE IF EXISTS "schema-ML_AutoRacer".list_account_type;

-- ------------------------------------------------------------------------

CREATE TABLE "schema-ML_AutoRacer".list_account_type (
	num serial NOT NULL,
	name_account_type varchar NOT NULL DEFAULT 'NA',
	CONSTRAINT list_account_type_pk PRIMARY KEY (num)
);

CREATE TABLE "schema-ML_AutoRacer".list_gender (
	num serial NOT NULL,
	name_gender varchar NOT NULL DEFAULT 'NA',
	CONSTRAINT list_gender_pk PRIMARY KEY (num)
);

CREATE TABLE "schema-ML_AutoRacer".list_country (
	num serial NOT NULL,
	name_country varchar NOT NULL DEFAULT 'NA',
	CONSTRAINT list_country_pk PRIMARY KEY (num)
);

CREATE TABLE "schema-ML_AutoRacer".list_password (
	num serial NOT NULL,
	user_password varchar NOT NULL DEFAULT 'NA',
	need_reset bool NOT NULL DEFAULT true,
    last_update timestamp NOT NULL,
	CONSTRAINT list_password_pk PRIMARY KEY (num)
);

CREATE TABLE "schema-ML_AutoRacer".list_details (
	num serial NOT NULL,
	user_name varchar NOT NULL DEFAULT 'NA',
	code_gender int NOT NULL DEFAULT 0,
	year_of_birth varchar NOT NULL DEFAULT 'NA',
	code_country int NOT NULL DEFAULT 0,
	user_email varchar NOT NULL DEFAULT 'NA',
    last_update timestamp NOT NULL,
    CONSTRAINT list_details_pk PRIMARY KEY (num),
	CONSTRAINT list_details_fk FOREIGN KEY (code_gender) REFERENCES "schema-ML_AutoRacer".list_gender(num) ON UPDATE CASCADE,
	CONSTRAINT list_details_fk_1 FOREIGN KEY (code_country) REFERENCES "schema-ML_AutoRacer".list_country(num) ON UPDATE CASCADE
);

CREATE TABLE "schema-ML_AutoRacer".user (
	username varchar NOT NULL DEFAULT 'NA',
	is_active bool NOT NULL DEFAULT true,
	code_account_type int NOT NULL DEFAULT 0,
	code_password int NOT NULL DEFAULT 0,
    code_details int NOT NULL DEFAULT 0,
	CONSTRAINT user_pk PRIMARY KEY (username),
	CONSTRAINT user_fk FOREIGN KEY (code_account_type) REFERENCES "schema-ML_AutoRacer".list_account_type(num) ON UPDATE CASCADE,
	CONSTRAINT user_fk_1 FOREIGN KEY (code_password) REFERENCES "schema-ML_AutoRacer".list_password(num) ON UPDATE CASCADE,
    CONSTRAINT user_fk_2 FOREIGN KEY (code_details) REFERENCES "schema-ML_AutoRacer".list_details(num) ON UPDATE CASCADE
);

-- ------------------------------------------------------------------------

CREATE VIEW "schema-ML_AutoRacer".view_user_account AS
SELECT
    "schema-ML_AutoRacer".user.username AS username,
    "schema-ML_AutoRacer".user.is_active AS account_active,
    "schema-ML_AutoRacer".list_account_type.name_account_type AS account_type,
    "schema-ML_AutoRacer".list_password.user_password AS account_password,
    "schema-ML_AutoRacer".list_password.need_reset AS password_require_reset,
    "schema-ML_AutoRacer".list_password.last_update AS password_update_on,
    "schema-ML_AutoRacer".user.code_details AS code_user_details
FROM
    "schema-ML_AutoRacer".user
LEFT OUTER JOIN "schema-ML_AutoRacer".list_account_type ON "schema-ML_AutoRacer".user.code_account_type = "schema-ML_AutoRacer".list_account_type.num
LEFT OUTER JOIN "schema-ML_AutoRacer".list_password ON "schema-ML_AutoRacer".user.code_password = "schema-ML_AutoRacer".list_password.num;

CREATE VIEW "schema-ML_AutoRacer".view_user_info AS
SELECT
    "schema-ML_AutoRacer".user.username AS username,
    "schema-ML_AutoRacer".list_details.user_name AS user_name,
    "schema-ML_AutoRacer".list_gender.name_gender AS user_gender,
    "schema-ML_AutoRacer".list_details.year_of_birth AS user_year_birth,
    "schema-ML_AutoRacer".list_country.name_country AS user_country,
    "schema-ML_AutoRacer".list_details.user_email AS user_email,
    "schema-ML_AutoRacer".list_details.last_update AS info_update_on
FROM
    "schema-ML_AutoRacer".user
LEFT OUTER JOIN "schema-ML_AutoRacer".list_details ON "schema-ML_AutoRacer".user.code_details = "schema-ML_AutoRacer".list_details.num
LEFT OUTER JOIN "schema-ML_AutoRacer".list_gender ON "schema-ML_AutoRacer".list_details.code_gender = "schema-ML_AutoRacer".list_gender.num
LEFT OUTER JOIN "schema-ML_AutoRacer".list_country ON "schema-ML_AutoRacer".list_details.code_country = "schema-ML_AutoRacer".list_country.num;
