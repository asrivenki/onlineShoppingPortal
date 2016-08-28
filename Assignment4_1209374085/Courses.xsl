<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/">
		<html>  <body>
			<h1>My Courses</h1>
			<table border="1">
				<tr bgcolor="yellow"> 
					<td><b>Name </b></td> 
					<td><b>Code</b></td> 
					<td><b>Level</b></td> 
					<td><b>Room</b></td> 
					<td><b>Cap</b></td>
				</tr>
				<xsl:for-each select="Courses/Course">
					<xsl:sort select="Name" />
					<tr style="font-size: 10pt; font-family: verdana">
						<td><xsl:value-of select="Name"/></td>
						<td><xsl:value-of select="Code"/></td>
						<td><xsl:value-of select="Level"/></td>
						<td><xsl:value-of select="Room"/></td>
						<td><xsl:value-of select="Cap"/></td>
					</tr>
				</xsl:for-each>
			</table>
		</body> </html>
	</xsl:template>
</xsl:stylesheet>
