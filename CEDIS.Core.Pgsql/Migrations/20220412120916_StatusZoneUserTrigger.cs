using Microsoft.EntityFrameworkCore.Migrations;

namespace CEDIS.Core.Pgsql.Migrations
{
    public partial class StatusZoneUserTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
create or replace function updateorderpicking()
	returns trigger
	language plpgsql
	as
$$
declare rowindex int := 0;
begin
	select coalesce((select userid from public.userzones where zoneid=new.zoneid and warehouseid = new.warehouseid and isactive=true),0) into STRICT rowindex;
	
	case when (rowindex>0) 
	then
		update public.orders set statusid=102 where statusid=101 and branchid = new.branchid and orderid = new.orderid and warehouseid = new.warehouseid and zoneid = new.zoneid;
		RETURN NEW;
	else
		RETURN NEW;
	end CASE;
end;
$$
");
            migrationBuilder.Sql($@"
CREATE or replace TRIGGER useronzone
  after insert
  ON public.orders
  FOR EACH ROW
  EXECUTE PROCEDURE updateorderpicking();

");
            migrationBuilder.Sql($@"
create or replace function updateorders()
	returns trigger
	language plpgsql
	as
$$
declare rowindex int := 0;
begin
	select coalesce((select count(userid) from public.userzones where zoneid=new.zoneid and warehouseid = new.warehouseid and isactive=true),0) into STRICT rowindex;
	
	case when (rowindex=0) 
	then
		update public.orders set statusid=101 where statusid = 102 and warehouseid = new.warehouseid and zoneid = new.zoneid;
		RETURN NEW;
	else
		update public.orders set statusid=102 where statusid=101 and warehouseid = new.warehouseid and zoneid = new.zoneid;
		RETURN NEW;
	end CASE;
end;
$$
");
            migrationBuilder.Sql($@"
CREATE or replace TRIGGER useractive
  after insert or update
  ON public.userzones
  FOR EACH ROW
  EXECUTE PROCEDURE updateorders();
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop trigger if exists useronzone on orders cascade");
            migrationBuilder.Sql("drop trigger if exists useractive on userzones cascade");
        }
    }
}
