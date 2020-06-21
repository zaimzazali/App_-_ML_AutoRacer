CREATE ROLE www WITH
	LOGIN
	NOSUPERUSER
	NOCREATEDB
	NOCREATEROLE
	INHERIT
	NOREPLICATION
	CONNECTION LIMIT -1
	PASSWORD 'password';

GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO www;

GRANT SELECT ON TABLE public.list_account_type TO www;
GRANT SELECT ON TABLE public.list_country TO www;
GRANT SELECT ON TABLE public.list_details TO www;
GRANT SELECT ON TABLE public.list_gender TO www;
GRANT SELECT ON TABLE public.list_password TO www;
GRANT SELECT ON TABLE public.list_user TO www;
GRANT INSERT ON TABLE public.list_account_type TO www;
GRANT INSERT ON TABLE public.list_country TO www;
GRANT INSERT ON TABLE public.list_details TO www;
GRANT INSERT ON TABLE public.list_gender TO www;
GRANT INSERT ON TABLE public.list_password TO www;
GRANT INSERT ON TABLE public.list_user TO www;

GRANT UPDATE ON TABLE public.list_password TO www;
GRANT UPDATE ON TABLE public.list_user TO www;

GRANT SELECT ON TABLE public.view_user_account TO www;
GRANT SELECT ON TABLE public.view_user_info TO www;
