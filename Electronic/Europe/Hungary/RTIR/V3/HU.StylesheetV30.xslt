<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fn="http://www.w3.org/2005/xpath-functions" xmlns:n1="http://schemas.nav.gov.hu/OSA/3.0/data" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xs="http://www.w3.org/2001/XMLSchema-instance" exclude-result-prefixes="xs xsd n1">
	<xsl:decimal-format name="european" decimal-separator="," grouping-separator="." NaN=""/>
	<xsl:output version="4.0" method="html" indent="no" encoding="UTF-8" doctype-public="-//W3C//DTD HTML 4.01 Transitional//EN" doctype-system="http://www.w3.org/TR/html4/loose.dtd"/>
	<xsl:param name="SV_OutputFormat" select="'HTML'"/>
	<xsl:variable name="H" select="/"/>
	<xsl:template match="/">
		<html>
			<head>
				<meta charset="UTF-8"/>
				<title>e-Invoice</title>
				<style type="text/css">body{display:table;margin:-10em 1em;background-color:#fff;font-family:'Tahoma',"Times New Roman",Times,serif;font-size:.7em;color:#666;min-width:960px}table,tr,th,td{border-spacing:0;border-width:0;border-style:none}h1{padding-bottom:3px;padding-top:3px;margin-bottom:3px;text-transform:uppercase;font-family:Arial,Helvetica,sans-serif;font-size:1.4em;text-transform:none} hr{height:1px;color:#000;background-color:#000;border-bottom:1px solid #000}.invInf{float:right;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse}.invInf th,.invInf td{text-align:left;border-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;padding:0.2em}.invLn{border-bottom-style:solid;border-width:1px;border-color:gray;border-collapse:collapse;width:100%}.invLn td,.invLn th{padding:.1em .5em;border-width:1px;border-bottom-style:solid;border-color:#ddd;border-collapse:collapse}.nwr{white-space:nowrap}.invLn tr{padding:0;border-width:1px;border-bottom-style:solid;border-color:#ddd;border-collapse:collapse;-moz-border-radius:}.invTtl{border-spacing:0;border-style:solid;border-color:#ddd;border-collapse:collapse;float:right}.invTtl td,.invTtl th{padding:.1em .5em;border-bottom-style:solid;border-width:1px;border-color:#ddd;border-collapse:collapse;border-spacing:0;text-align:right}.invTtl2{border-spacing:0;border-style:solid;border-color:#ddd;border-collapse:collapse;float:left}.invTtl2 td,.invTtl2 th{padding:.1em .5em;border-bottom-style:solid;border-width:1px;border-color:#ddd;border-collapse:collapse;border-spacing:0;text-align:left}.taxExm{border-spacing:0;border-style:inset;border-color:black;border-collapse:collapse;float:left}.taxExm td,.taxExm th{padding:.1em .5em;border-style:inset;border-width:2px;border-color:gray;border-collapse:collapse;border-spacing:0;text-align:right}.ntsTbl{border-style:inset;border-width:1px;border-color:#ddd;border-collapse:collapse;width:100%;height:100px}.ntsTbl td{border-style:inset;border-width:0;border-color:#ddd;border-collapse:collapse;height:100%;vertical-align:top}ul.noind{margin:.5em 0 .5em .5em;padding:0 0 0 1em}div.mLay{display:table}.ar{text-align:right}.inv{margin:10em 0;width:100%}.mLay{width:100%;margin:1em 0}.item,.item tr,.item th,.item td{border-style:none;margin:0;padding:0;text-align:left}.item td{padding-bottom:.3em}@media print{.inv{page-break-after:always;margin:0}body{margin:0}}</style>
			</head>
			<body>
				<xsl:for-each select="$H">
					<xsl:for-each select="//n1:InvoiceData">

						<div class="inv">
							<table class="mLay" cellspacing="0px" cellpadding="0px">
								<tbody>
									<tr style="vertical-align:top;">
										<td style="width:35%;" align="center" valign="middle"> 
											<!--
											<img alt="Logo" src="" />
											-->
										</td>
										<td style="width:30%" align="center" valign="middle">
											<h1 align="center">
												<span style="font-weight:400;  font-size: 18pt">
													<xsl:text>Hungary e-Invoice Ver 3.0</xsl:text>
												</span>
											</h1>
										</td>
										<td style="width:35%;" align="center" valign="middle">
											<img alt="HU NAV" src="data:image/png;base64,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"/>

										</td>
									</tr>
									<tr style="vertical-align:top;">
										<td style="width:35%;vertical-align:middle;">
											<table>
												<tbody>
													<xsl:for-each select="n1:invoiceMain/n1:invoice/n1:invoiceHead/n1:supplierInfo">
														<!--<xsl:apply-templates/>-->
														<h1>
															<span style="font-weight:400">
																<xsl:value-of select="n1:supplierName"/>
															</span>
														</h1>
														<br/>
														<b>
															<xsl:text>Tax Number : </xsl:text>
														</b>
														<xsl:value-of select="n1:supplierTaxNumber/n1:taxpayerId"/>
														<xsl:if test="n1:supplierTaxNumber/n1:vatCode">
															<br/>
															<b>
																<xsl:text>Vat Code : </xsl:text>
															</b>
															<xsl:value-of select="n1:supplierTaxNumber/n1:vatCode"/>
														</xsl:if>
														<xsl:if test="n1:supplierTaxNumber/n1:countyCode">
															<br/>
															<b>
																<xsl:text>Country Code : </xsl:text>
															</b>
															<xsl:value-of select="n1:supplierTaxNumber/n1:countyCode"/>
														</xsl:if>
														<xsl:if test="n1:supplierAddress/n1:detailedAddress/n1:streetName">
															<br/>
															<b>
																<xsl:text>Street Name : </xsl:text>
															</b>
															<xsl:value-of select="n1:supplierAddress/n1:detailedAddress/n1:streetName"/>
														</xsl:if>



														<xsl:if test="n1:supplierAddress/n1:detailedAddress/n1:number">
															<br/>
															<b>
																<xsl:text>Street Number : </xsl:text>
															</b>
															<xsl:value-of select="n1:supplierAddress/n1:detailedAddress/n1:number"/>
														</xsl:if>



														<xsl:if test="n1:supplierAddress/n1:detailedAddress/n1:publicPlaceCategory">
															<br/>
															<b>
																<xsl:text>Public Place Category : </xsl:text>
															</b>
															<xsl:value-of select="n1:supplierAddress/n1:detailedAddress/n1:publicPlaceCategory"/>
														</xsl:if>
														<xsl:if test="n1:supplierAddress/n1:detailedAddress/n1:city">
															<br/>
															<b>
																<xsl:text>City : </xsl:text>
															</b>
															<xsl:value-of select="n1:supplierAddress/n1:detailedAddress/n1:city"/>
														</xsl:if>
														<xsl:if test="n1:supplierAddress/n1:detailedAddress/n1:postalCode">
															<br/>
															<b>
																<xsl:text> Postal Code : </xsl:text>
															</b>
															<xsl:value-of select="n1:supplierAddress/n1:detailedAddress/n1:postalCode"/>
														</xsl:if>
													</xsl:for-each>
												</tbody>
											</table>

										</td>

										<td style="width:35%;vertical-align:middle;">
											<table>
												<tbody>
													<xsl:for-each select="n1:invoiceMain/n1:invoice/n1:invoiceHead/n1:customerInfo">
														<!--<xsl:apply-templates/>-->
														<h1>
															<span style="font-weight:400">
																<xsl:value-of select="n1:customerName"/>
															</span>
														</h1>
														<br/>
														<b>
															<xsl:text>Tax Number : </xsl:text>
														</b>
														<xsl:value-of select="n1:customerTaxNumber/n1:taxpayerId"/>
														<xsl:if test="n1:customerTaxNumber/n1:vatCode">
															<br/>
															<b>
																<xsl:text>Vat Code : </xsl:text>
															</b>
															<xsl:value-of select="n1:customerTaxNumber/n1:vatCode"/>
														</xsl:if>
														<xsl:if test="n1:customerTaxNumber/n1:countyCode">
															<br/>
															<b>
																<xsl:text>Country Code : </xsl:text>
															</b>
															<xsl:value-of select="n1:customerTaxNumber/n1:countyCode"/>
														</xsl:if>
														<xsl:if test="n1:customerAddress/n1:detailedAddress/n1:streetName">
															<br/>
															<b>
																<xsl:text>Street Name : </xsl:text>
															</b>
															<xsl:value-of select="n1:customerAddress/n1:detailedAddress/n1:streetName"/>
														</xsl:if>
														<xsl:if test="n1:customerAddress/n1:detailedAddress/n1:publicPlaceCategory">
															<br/>

															<b>
																<xsl:text>Street Number : </xsl:text>
															</b>
															<xsl:value-of select="n1:customerAddress/n1:detailedAddress/n1:number"/>
														</xsl:if>
														<xsl:if test="n1:customerAddress/n1:detailedAddress/n1:number">
															<br/>


															<b>
																<xsl:text>Public Place Category : </xsl:text>
															</b>
															<xsl:value-of select="n1:customerAddress/n1:detailedAddress/n1:publicPlaceCategory"/>
														</xsl:if>
														<xsl:if test="n1:customerAddress/n1:detailedAddress/n1:city">
															<br/>
															<b>
																<xsl:text>City : </xsl:text>
															</b>
															<xsl:value-of select="n1:customerAddress/n1:detailedAddress/n1:city"/>
														</xsl:if>
														<xsl:if test="n1:customerAddress/n1:detailedAddress/n1:postalCode">
															<br/>
															<b>
																<xsl:text> Postal Code : </xsl:text>
															</b>
															<xsl:value-of select="n1:customerAddress/n1:detailedAddress/n1:postalCode"/>
														</xsl:if>
													</xsl:for-each>
												</tbody>
											</table>											
										</td>
										<td style="width:30%;vertical-align:middle;">
											<table>
												<tbody>
													<xsl:if test="n1:invoiceNumber">
														<h1>
															<xsl:text>Invoice Number : </xsl:text>
															<xsl:value-of select="n1:invoiceNumber"/>	
														</h1>
													</xsl:if>
													<br/>
													<xsl:if test="n1:invoiceIssueDate">
														<b>
															<xsl:text>Invoice Date : </xsl:text>
														</b>
														<xsl:value-of select="n1:invoiceIssueDate"/>	
													</xsl:if>
													<br/>
													<xsl:for-each select="n1:invoiceMain/n1:invoice/n1:invoiceHead/n1:invoiceDetail">														
														<xsl:for-each select="n1:additionalInvoiceData/n1:dataName">
															<xsl:if test="contains(.,'NEF_HEADER_OPERATIONTYPE')">
																<b>
																	<xsl:text>Operation Type : </xsl:text>
																</b>
																<xsl:value-of select="../n1:dataValue"/>	
															</xsl:if>
														</xsl:for-each>
														<br/>
														<xsl:if test="n1:invoiceCategory">
															<b>
																<xsl:text>Invoice Category : </xsl:text>
															</b>
															<xsl:value-of select="n1:invoiceCategory"/>	
														</xsl:if>
														<br/>
														<xsl:if test="n1:invoiceAppearance">
															<b>
																<xsl:text>Invoice Appearance : </xsl:text>
															</b>
															<xsl:value-of select="n1:invoiceAppearance"/>	
														</xsl:if>
														<br/>
														<xsl:if test="n1:currencyCode">
															<b>
																<xsl:text>Currency Code : </xsl:text>
															</b>
															<xsl:value-of select="n1:currencyCode"/>	
														</xsl:if>
														<br/>
														<xsl:for-each select="../../n1:invoiceReference">
															<xsl:if test="n1:originalInvoiceNumber">
																<b>
																	<xsl:text>Original Invoice Number : </xsl:text>
																</b>
																<xsl:value-of select="n1:originalInvoiceNumber"/>	
																<br/>
															</xsl:if>

															<xsl:if test="n1:modificationIssueDate">
																<b>
																	<xsl:text>Modification Issue Date : </xsl:text>
																</b>
																<xsl:value-of select="n1:modificationIssueDate"/>
																<br/>																
															</xsl:if>

															<xsl:if test="n1:modificationTimestamp">
																<b>
																	<xsl:text>Modification Timestamp : </xsl:text>
																</b>
																<xsl:value-of select="n1:modificationTimestamp"/>	
																<br/>
															</xsl:if>

															<xsl:if test="n1:modifyWithoutMaster">
																<b>
																	<xsl:text>Modify Without Master : </xsl:text>
																</b>
																<xsl:value-of select="n1:modifyWithoutMaster"/>	
																<br/>
															</xsl:if>

															<xsl:if test="n1:modificationIndex">
																<b>
																	<xsl:text>Modification Index : </xsl:text>
																</b>
																<xsl:value-of select="n1:modificationIndex"/>	
																<br/>
															</xsl:if>
														</xsl:for-each>
														<xsl:if test="n1:invoiceIssueDate">
															<b>
																<xsl:text>Issue Date : </xsl:text>
															</b>
															<xsl:value-of select="n1:invoiceIssueDate"/>	
														</xsl:if>
														<br/>
														<xsl:if test="n1:invoiceDeliveryDate">
															<b>
																<xsl:text>Delivery Date : </xsl:text>
															</b>
															<xsl:value-of select="n1:invoiceDeliveryDate"/>	
														</xsl:if>
														<br/>


														<xsl:if test="n1:paymentDate">
															<b>
																<xsl:text>Payment Date : </xsl:text>
															</b>
															<xsl:value-of select="n1:paymentDate"/>	
														</xsl:if>
														<br/>
														<xsl:if test="n1:paymentMethod">
															<b>
																<xsl:text>Payment Method : </xsl:text>
															</b>
															<xsl:value-of select="n1:paymentMethod"/>	
														</xsl:if>
														<br/>
														<xsl:for-each select="n1:additionalInvoiceData/n1:dataName"> 
															<xsl:if test="contains(.,'NEF_HEADER_UUID')">
																<b>
																	<xsl:text>UUID : </xsl:text>
																</b>
																<xsl:value-of select="../n1:dataValue"/>	
															</xsl:if>

															<xsl:if test="contains(.,'D00001_NEF_HEADER_DESPATCHNUMBERANDDATE') and not(../../n1:additionalInvoiceData/n1:dataName[text()='D00002_NEF_HEADER_DESPATCHNUMBERANDDATE'])">

																<br/>
																<b>
																Despatch Number : 
																</b>
																<xsl:value-of select="substring-before(../n1:dataValue,',')"/>	
																<b>
																	<br/>
																	<xsl:text>Despatch Date : </xsl:text>
																</b>
																<xsl:value-of select="substring-after(../n1:dataValue,',')"/>	

															</xsl:if>


															<xsl:if test="contains(.,'NEF_HEADER_ORDERNUMBERANDDATE')">
																<br/>
																<b>
																	<xsl:text>Order Number : </xsl:text>
																</b>
																<xsl:value-of select="substring-before(../n1:dataValue,',')"/>	
																<b>
																	<br/>
																	<xsl:text>Order Date : </xsl:text>
																</b>
																<xsl:value-of select="substring-after(../n1:dataValue,',')"/>	
															</xsl:if>
														</xsl:for-each>
													</xsl:for-each>
												</tbody>
											</table>
										</td>
									</tr>
								</tbody>
							</table>
							<div class="mLay">
								<table class="invLn">
									<tbody>
										<tr>
											<th style="color:#dd4b39">
												<xsl:text>Line Number</xsl:text>
											</th>
											<xsl:if test="n1:invoiceMain/n1:invoice/n1:invoiceLines/n1:line/n1:productCodes/n1:productCode[.!='']">
												<th style="color:#dd4b39">
													<xsl:text>Product Code Category - Own Value</xsl:text>
												</th>
											</xsl:if>
											<th style="color:#dd4b39">
												<xsl:text>Line Description</xsl:text>
											</th>
											<xsl:if test="n1:invoiceMain/n1:invoice/n1:invoiceLines/n1:line/n1:lineModificationReference[.!='']">
												<th style="color:#dd4b39">
													<xsl:text>Ref Line Number</xsl:text>
												</th>
												<th style="color:#dd4b39">
													<xsl:text>Opr. Type</xsl:text>
												</th>
											</xsl:if>
											<th style="color:#dd4b39">
												<xsl:text>Quantity</xsl:text>
											</th>
											<th style="color:#dd4b39">
												<xsl:text>Unit Of Measure</xsl:text>
											</th>
											<xsl:if test="n1:invoiceMain/n1:invoice/n1:invoiceLines/n1:line/n1:unitPrice">
												<th style="color:#dd4b39">
													<xsl:text>Unit Price</xsl:text>
												</th>
											</xsl:if>
											<xsl:if test="n1:invoiceMain/n1:invoice/n1:invoiceLines/n1:line/n1:lineAmountsNormal/n1:lineNetAmountData/n1:lineNetAmount">
												<th style="color:#dd4b39">
													<xsl:text>Net Amount</xsl:text>
												</th>
											</xsl:if>
											<xsl:if test="n1:invoiceMain/n1:invoice/n1:invoiceLines/n1:line/n1:lineAmountsNormal/n1:lineVatRate/n1:vatPercentage">
												<th style="color:#dd4b39">
													<xsl:text>Vat Percentage</xsl:text>
												</th>
											</xsl:if>
											<xsl:if test="n1:invoiceMain/n1:invoice/n1:invoiceLines/n1:line/n1:lineAmountsNormal/n1:lineVatData/n1:lineVatAmount">
												<th style="color:#dd4b39">
													<xsl:text>Vat Amount</xsl:text>
												</th>
											</xsl:if>
											<xsl:if test="n1:invoiceMain/n1:invoice/n1:invoiceLines/n1:line/n1:lineAmountsNormal/n1:lineVatData/n1:lineVatAmountHUF">
												<th style="color:#dd4b39">
													<xsl:text>Vat Amount (HUF)</xsl:text>
												</th>
											</xsl:if>
											<th style="color:#dd4b39">
												<xsl:text>Gross Amount</xsl:text>
											</th>
										</tr>
										<xsl:apply-templates select="n1:invoiceMain/n1:invoice/n1:invoiceLines"/>
									</tbody>
								</table>
								<br/>
								<table class="invTtl">
									<xsl:if test="n1:invoiceMain/n1:invoice/n1:invoiceSummary/n1:summaryNormal/n1:invoiceNetAmount">
										<xsl:for-each select="n1:invoiceMain/n1:invoice/n1:invoiceSummary/n1:summaryNormal/n1:invoiceNetAmount">
											<tr>
												<th  style="color:#dd4b39">
													<xsl:text>Total Net Amount</xsl:text>
												</th>
												<td>
													<xsl:call-template name="sTA"/>
												</td>
											</tr>
										</xsl:for-each>
									</xsl:if>

									<xsl:for-each select="n1:invoiceMain/n1:invoice/n1:invoiceSummary/n1:summaryNormal">
										<xsl:for-each select="n1:summaryByVatRate">
											<tr>
												<th  style="color:#dd4b39">
													<xsl:text>Tax Amount</xsl:text>
													<xsl:if test="n1:vatRate/n1:vatPercentage!=''">
														<xsl:text> (</xsl:text>
														<xsl:value-of select="n1:vatRate/n1:vatPercentage*100"/>
														<xsl:text>%)</xsl:text>
													</xsl:if>

												</th>
												<td>
													<xsl:for-each select="n1:vatRateVatData/n1:vatRateVatAmount">
														<xsl:call-template name="sTA"/>
													</xsl:for-each>
												</td>
											</tr>
											<tr>
												<th  style="color:#dd4b39">
													<xsl:text>Vat Rate Net Amount</xsl:text>
													<xsl:if test="n1:vatRate/n1:vatPercentage!=''">
														<xsl:text> (</xsl:text>
														<xsl:value-of select="n1:vatRate/n1:vatPercentage*100"/>
														<xsl:text>%)</xsl:text>
													</xsl:if>

												</th>
												<td>
													<xsl:for-each select="n1:vatRateNetData/n1:vatRateNetAmount">
														<xsl:call-template name="sTA"/>
													</xsl:for-each>
												</td>
											</tr>
										</xsl:for-each>
										<xsl:if test="n1:invoiceVatAmount">
											<tr>
												<th  style="color:#dd4b39">
													<xsl:text>Total Vat Amount</xsl:text>
												</th>
												<td>
													<xsl:for-each select="n1:invoiceVatAmount">
														<xsl:call-template name="sTA"/>
													</xsl:for-each>
												</td>
											</tr>
										</xsl:if>
									</xsl:for-each>

									<tr>
										<th  style="color:#dd4b39">
											<xsl:text>Total Gross Amount</xsl:text>
										</th>
										<td>
											<xsl:for-each select="n1:invoiceMain/n1:invoice/n1:invoiceSummary/n1:summaryGrossData/n1:invoiceGrossAmount">
												<xsl:call-template name="sTA"/>
											</xsl:for-each>
										</td>
									</tr>

								</table>
								<xsl:if test="n1:invoiceMain/n1:invoice/n1:invoiceHead/n1:invoiceDetail/n1:additionalInvoiceData/n1:dataName[text()='D00002_NEF_HEADER_DESPATCHNUMBERANDDATE']">
									<table id="irsaliyeListesi" class="invTtl2">
										<tbody>
											<tr>
												<th style="color:#dd4b39">
													<xsl:text>Despatch Date</xsl:text>
												</th>
												<th style="color:#dd4b39">
													<xsl:text>Despatch Number</xsl:text>
												</th>
											</tr>
											<xsl:for-each select="n1:invoiceMain/n1:invoice/n1:invoiceHead/n1:invoiceDetail/n1:additionalInvoiceData">
												<xsl:if test="contains(n1:dataName,'NEF_HEADER_DESPATCHNUMBERANDDATE')">
													<tr>
														<td>
															<xsl:value-of select="substring-before(n1:dataValue,',')"/>
														</td>
														<td>
															<xsl:value-of select="substring-after(n1:dataValue,',')"/>
														</td>
													</tr>
												</xsl:if>
											</xsl:for-each>
										</tbody>
									</table>
								</xsl:if>

							</div>
							<xsl:if test="n1:invoiceMain/n1:invoice/n1:invoiceHead/n1:invoiceDetail/n1:additionalInvoiceData/n1:dataName[text()='D00001_NEF_HEADER_DESC']">
								<table style="mLay;">
									<tbody>
										<br/>
										<table>
											<tbody>
												<tr align="left">
													<td>
														<b style="color:#dd4b39" >
															<xsl:text>Note :</xsl:text>
														</b>
														<br/>
														<xsl:for-each select="n1:invoiceMain/n1:invoice/n1:invoiceHead/n1:invoiceDetail/n1:additionalInvoiceData">
															<xsl:if test="contains(.,'NEF_HEADER_DESC')">
																<br/>
																<font style="margin-left:1em;">
																	<xsl:value-of select="n1:dataValue"/>
																</font>	
															</xsl:if>
														</xsl:for-each>
													</td>
												</tr>
											</tbody>
										</table>

									</tbody>
								</table>
							</xsl:if>
						</div>
					</xsl:for-each>
				</xsl:for-each>
			</body>
		</html>
	</xsl:template>		
	<xsl:template name="sRA">
		<xsl:value-of select="format-number(sum(.),'###.##0,00','european')"/>

	</xsl:template>
	<xsl:template name="sRAU">
		<xsl:value-of select="format-number(sum(.),'###.##0,0000','european')"/>

	</xsl:template>
	<xsl:template name="sRAV">
		<xsl:value-of select="format-number(sum(.)*100,'###.##','european')"/>

	</xsl:template>
	<xsl:template name="sTA">
		<xsl:value-of select="format-number(sum(.),'###.##0,00','european')"/>

	</xsl:template>
	<xsl:template name="sDt">
		<xsl:value-of select="substring(.,9,2)"/>
		<xsl:text>-</xsl:text>
		<xsl:value-of select="substring(.,6,2)"/>
		<xsl:text>-</xsl:text>
		<xsl:value-of select="substring(.,1,4)"/>
	</xsl:template>


	<xsl:template match="n1:line">
		<tr>

			<td class="ar">
				<xsl:value-of select="n1:lineNumber"/>
			</td>
			<xsl:if test="n1:productCodes/n1:productCode[.!='']">
				<td>
					<xsl:value-of select="n1:productCodes/n1:productCode/n1:productCodeCategory"/>
				-
					<xsl:value-of select="n1:productCodes/n1:productCode/n1:productCodeOwnValue"/>
				</td>
			</xsl:if>

			<xsl:if test="n1:lineDescription[.!='']">
				<td>
					<xsl:value-of select="n1:lineDescription"/>
				</td>
			</xsl:if>

			<xsl:if test="n1:lineModificationReference[.!='']">
				<td>
					<xsl:value-of select="n1:lineModificationReference/n1:lineNumberReference"/>
				</td>
				<td>
					<xsl:value-of select="n1:lineModificationReference/n1:lineOperation"/>
				</td>
			</xsl:if>
			<td class="ar">
				<xsl:value-of select="format-number(n1:quantity,'###.###,##','european')"/>
			</td>
			<td>
				<xsl:for-each select="n1:unitOfMeasure">
					<xsl:call-template name="cUni"/>
				</xsl:for-each>
			</td>
			<td class="ar">
				<xsl:for-each select="n1:unitPrice">
					<xsl:call-template name="sRAU"/>
				</xsl:for-each>
			</td>
			<if test="n1:lineAmountsNormal/n1:lineNetAmountData/n1:lineNetAmount">
				<td class="ar">
					<xsl:for-each select="n1:lineAmountsNormal/n1:lineNetAmountData/n1:lineNetAmount">
						<xsl:call-template name="sRA"/>
					</xsl:for-each>
				</td>
			</if>
			<if test="n1:lineAmountsNormal/n1:lineVatRate/n1:vatPercentage">
				<td class="ar">
					<xsl:for-each select="n1:lineAmountsNormal/n1:lineVatRate/n1:vatPercentage">
						<xsl:call-template name="sRAV"/> %
					</xsl:for-each>
				</td>
			</if>
			<if test="n1:lineAmountsNormal/n1:lineVatData/n1:lineVatAmount">
				<td class="ar">
					<xsl:for-each select="n1:lineAmountsNormal/n1:lineVatData/n1:lineVatAmount">
						<xsl:call-template name="sRA"/>
					</xsl:for-each>
				</td>
			</if>
			<if test="n1:lineAmountsNormal/n1:lineVatData/n1:lineVatAmountHUF">
				<td class="ar">
					<xsl:for-each select="n1:lineAmountsNormal/n1:lineVatData/n1:lineVatAmountHUF">
						<xsl:call-template name="sRA"/>
					</xsl:for-each>
				</td>
			</if>
			<td class="ar">
				<xsl:for-each select="n1:lineAmountsNormal/n1:lineGrossAmountData/n1:lineGrossAmountNormal">
					<xsl:call-template name="sRA"/>
				</xsl:for-each>
			</td>
		</tr>
	</xsl:template>


	<!--<xsl:template name="cUni1">
		<xsl:choose>
				<xsl:when test="n1:unitOfMeasure='KWH'">
					<xsl:text>&#160;KWH</xsl:text>
				</xsl:when>
				<xsl:when test="n1:unitOfMeasure='C62'">
					<xsl:text>&#160;Unit</xsl:text>
				</xsl:when>
				<xsl:otherwise>
					<xsl:text/>
					<xsl:value-of select="n1:unitOfMeasure"/>
				</xsl:otherwise>
			</xsl:choose>
	</xsl:template>-->

	<xsl:template name="cUni">
		<xsl:choose>
			<xsl:when test=".='26'">
				<xsl:text>&#160;Ton</xsl:text>
			</xsl:when>
			<xsl:when test=".='BX'">
				<xsl:text>&#160;Box</xsl:text>
			</xsl:when>
			<xsl:when test=".='NIU'">
				<xsl:text>&#160;Unit</xsl:text>
			</xsl:when>
			<xsl:when test=".='KGM'">
				<xsl:text>&#160;Kg</xsl:text>
			</xsl:when>
			<xsl:when test=".='KJO'">
				<xsl:text>&#160;kJ</xsl:text>
			</xsl:when>
			<xsl:when test=".='GRM'">
				<xsl:text>&#160;gr.</xsl:text>
			</xsl:when>
			<xsl:when test=".='MGM'">
				<xsl:text>&#160;MG</xsl:text>
			</xsl:when>
			<xsl:when test=".='NT'">
				<xsl:text>&#160;Net Tonnage</xsl:text>
			</xsl:when>
			<xsl:when test=".='GT'">
				<xsl:text>&#160;GT</xsl:text>
			</xsl:when>
			<xsl:when test=".='MTR'">
				<xsl:text>&#160;M</xsl:text>
			</xsl:when>
			<xsl:when test=".='MMT'">
				<xsl:text>&#160;MM</xsl:text>
			</xsl:when>
			<xsl:when test=".='KTM'">
				<xsl:text>&#160;KM</xsl:text>
			</xsl:when>
			<xsl:when test=".='MLT'">
				<xsl:text>&#160;ML</xsl:text>
			</xsl:when>
			<xsl:when test=".='MMQ'">
				<xsl:text>&#160;MM3</xsl:text>
			</xsl:when>
			<xsl:when test=".='CLT'">
				<xsl:text>&#160;CL</xsl:text>
			</xsl:when>
			<xsl:when test=".='CMK'">
				<xsl:text>&#160;CM2</xsl:text>
			</xsl:when>
			<xsl:when test=".='CMQ'">
				<xsl:text>&#160;CM3</xsl:text>
			</xsl:when>
			<xsl:when test=".='CMT'">
				<xsl:text>&#160;CM</xsl:text>
			</xsl:when>
			<xsl:when test=".='MTK'">
				<xsl:text>&#160;M2</xsl:text>
			</xsl:when>
			<xsl:when test=".='MTQ'">
				<xsl:text>&#160;M3</xsl:text>
			</xsl:when>
			<xsl:when test=".='DAY'">
				<xsl:text>&#160;Day</xsl:text>
			</xsl:when>
			<xsl:when test=".='MON'">
				<xsl:text>&#160;Month</xsl:text>
			</xsl:when>
			<xsl:when test=".='PA'">
				<xsl:text>&#160;Package</xsl:text>
			</xsl:when>
			<xsl:when test=".='KWH'">
				<xsl:text>&#160;KWH</xsl:text>
			</xsl:when>
			<xsl:when test=".='C62'">
				<xsl:text>&#160;Unit</xsl:text>
			</xsl:when>
			<xsl:otherwise>
				<xsl:text/>
				<xsl:value-of select="."/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>